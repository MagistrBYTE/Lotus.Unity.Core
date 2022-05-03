//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusTreeViewControl.cs
*		Элемент для отображения дерева для редактора Unity.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;
using UnityEditor;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace Editor
	{
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Элемент для отображения дерева для редактора Unity
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CTreeViewControl
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal CCollectionViewHierarchyObject mRoot;
			protected internal System.Object mRootData;
			protected internal CViewItemHierarchyObject mSelectedNode;

			// Параметры фильтрации
			protected internal String mSearchString;
			protected internal TStringSearchOption mSearchOption;

			// Параметры отображения
			protected internal Boolean mIsCheckableView;
			protected internal Boolean mIsIconableView;
			protected internal Func<CViewItemHierarchyObject, Texture> mIconDelegate;

			// События
			protected internal Action<CViewItemHierarchyObject> mOnSelectedNode;

			// Служебные данные для отрисовки
			protected internal Rect mControlRect;
			protected internal Single mStartDrawY;
			protected internal Single mHeight;
			protected internal Int32 mControlID;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Корневой узел дерева
			/// </summary>
			public CCollectionViewHierarchyObject Root
			{
				get { return (mRoot); }
				set 
				{ 
					mRoot = value;
					if (mRoot != null)
					{
						mRootData = mRoot.Source;
					}
				}
			}

			/// <summary>
			/// Текущий выбранный узел отображения
			/// </summary>
			public CViewItemHierarchyObject SelectedNode
			{
				get { return (mSelectedNode); }
			}


			/// <summary>
			/// Текущий выбранный узел отображения
			/// </summary>
			public CFileSystemDirectory SelectedDirectory
			{
				get { return (mSelectedNode?.DataContext as CFileSystemDirectory); }
			}

			/// <summary>
			/// Текущий выбранный узел отображения
			/// </summary>
			public CFileSystemFile SelectedFile
			{
				get { return (mSelectedNode?.DataContext as CFileSystemFile); }
			}

			//
			// ПАРАМЕТРЫ ФИЛЬТРАЦИИ
			//
			/// <summary>
			/// Строка для фильтрования и поиска узлов по имени
			/// </summary>
			public String SearchString
			{
				get { return (mSearchString); }
				set
				{
					if (mSearchString != value)
					{
						mSearchString = value;
						OnFiltered();
					}
				}
			}

			/// <summary>
			/// Опции поиска узлов по имени
			/// </summary>
			public TStringSearchOption SearchOption
			{
				get { return (mSearchOption); }
				set
				{
					if (mSearchOption != value)
					{
						mSearchOption = value;
						OnFiltered();
					}
				}
			}

			//
			// ПАРАМЕТРЫ ОТОБРАЖЕНИЯ
			//
			/// <summary>
			/// Отображение флажка выбора элемента узла
			/// </summary>
			public Boolean IsCheckableView
			{
				get { return (mIsCheckableView); }
				set
				{
					if (mIsCheckableView != value)
					{
						mIsCheckableView = value;
					}
				}
			}

			/// <summary>
			/// Отображение иконки элемента узла
			/// </summary>
			public Boolean IsIconableView
			{
				get { return (mIsIconableView); }
				set
				{
					if (mIsIconableView != value)
					{
						mIsIconableView = value;
					}
				}
			}

			/// <summary>
			/// Делегат для представления иконки в зависимости от типа узла отображения
			/// </summary>
			public Func<CViewItemHierarchyObject, Texture> IconDelegate
			{
				get { return (mIconDelegate); }
				set
				{
					if (mIconDelegate != value)
					{
						mIconDelegate = value;
					}
				}
			}

			//
			// СОБЫТИЯ
			//
			/// <summary>
			/// Событие выбор элемента узла
			/// </summary>
			public Action<CViewItemHierarchyObject> OnSelectedNode
			{
				get { return (mOnSelectedNode); }
				set
				{
					if (mOnSelectedNode != value)
					{
						mOnSelectedNode = value;
					}
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CTreeViewControl()
			{
				Root = new CCollectionViewHierarchyObject();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="data">Данные корневого узла</param>
			//---------------------------------------------------------------------------------------------------------
			public CTreeViewControl(System.Object data)
			{
				Root = new CCollectionViewHierarchyObject();
				Root.Source = data;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рисование дерева в автоматическом макете
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void DrawTreeLayout()
			{
				if (mRoot != null)
				{
					mHeight = 0;
					mStartDrawY = 0;
					mRoot.Visit(match: OnGetLayoutHeight);

					mControlRect = EditorGUILayout.GetControlRect(false, mHeight);
					mControlID = GUIUtility.GetControlID(FocusType.Passive, mControlRect);
					mRoot.Visit(match: OnDrawRow);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Фильтрация узлов
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void OnFiltered()
			{
				if (String.IsNullOrEmpty(mSearchString) && mRootData != null)
				{
					mRoot.IsFiltered = false;
					mRoot.ResetSource();
				}
				else
				{
					mRoot.IsFiltered = true;
					mRoot.Filter =(System.Object data)=>
					{
						ILotusFileSystemEntity file_system_entity = data as ILotusFileSystemEntity;
						return (file_system_entity.Name.Contains(mSearchString));
					};

					mRoot.ResetSource();
					mRoot.Expanded();
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ РИСОВАНИЯ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение высоты узла дерева
			/// </summary>
			/// <param name="node_view">Узел дерева</param>
			/// <returns>Высота</returns>
			//---------------------------------------------------------------------------------------------------------
			protected virtual Single GetRowHeight(ILotusViewItemHierarchy node_view)
			{
				return (EditorGUIUtility.singleLineHeight + 2);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вычисление высоты дерева
			/// </summary>
			/// <param name="node_view">Узел дерева</param>
			/// <returns>Статус продолжения вычисления высоты дерева</returns>
			//---------------------------------------------------------------------------------------------------------
			protected virtual Boolean OnGetLayoutHeight(ILotusViewItemHierarchy node_view)
			{
				if(node_view is CViewItemHierarchyObject node_object)
				{
					if (node_object.DataContext == null) return true;
				}

				mHeight += GetRowHeight(node_view);
				return (node_view.IsExpanded);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рисование узла дерева
			/// </summary>
			/// <param name="node_view">Узел дерева</param>
			/// <returns>Статус раскрытия данного узла</returns>
			//---------------------------------------------------------------------------------------------------------
			protected virtual Boolean OnDrawRow(ILotusViewItemHierarchy node_view)
			{
				if (node_view is CViewItemHierarchyObject node_object)
				{
					if (node_object.DataContext == null) return true;
				}

				Single row_indent = 16 * node_view.Level;
				Single row_height = GetRowHeight(node_view);

				Rect row_rect = new Rect(2, mControlRect.y + mStartDrawY, mControlRect.width, row_height);
				Rect indent_rect = new Rect(row_indent, mControlRect.y + mStartDrawY, mControlRect.width - row_indent, row_height);

				// render
				if (mSelectedNode == node_view)
				{
					GUI.Box(row_rect, GUIContent.none, XEditorStyles.BOX_FLOW_NODE_0_ON);
				}

				OnDrawTreeNode(indent_rect, node_view as CViewItemHierarchyObject, mSelectedNode == node_view, false);

				// test for events
				EventType event_type = Event.current.GetTypeForControl(mControlID);
				if (event_type == EventType.MouseUp && row_rect.Contains(Event.current.mousePosition))
				{
					mSelectedNode = node_view as CViewItemHierarchyObject;

					if (mOnSelectedNode != null) mOnSelectedNode(mSelectedNode);

					GUI.changed = true;
					Event.current.Use();
				}

				mStartDrawY += row_height;
				return node_view.IsExpanded;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рисование данных узла дерева
			/// </summary>
			/// <param name="rect">Прямоугольник вывода</param>
			/// <param name="node_view">Узел дерева</param>
			/// <param name="selected">Статус выбора узла</param>
			/// <param name="focus">Статус фокуса узла</param>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void OnDrawTreeNode(Rect rect, CViewItemHierarchyObject node_view, Boolean selected, Boolean focus)
			{
				GUIContent label_сontent = new GUIContent(node_view.DataContext.ToString());

				if(mIsIconableView && mIconDelegate != null)
				{
					label_сontent.image = mIconDelegate(node_view);
				}
				else
				{
					// Специальные случае
					if (node_view.DataContext is CFileSystemDirectory)
					{
						label_сontent.image = EditorGUIUtility.IconContent(XEditorStyles.ICON_FOLDER).image;
					}
					else
					{
						if (node_view.DataContext is CFileSystemFile system_file)
						{
							label_сontent.image = XEditorInspector.GetIconForFile(system_file.Info.Extension);
						}
					}
				}

				if (!node_view.IsLeaf)
				{
					Rect rect_foldout = new Rect(rect.x, rect.y, 12, rect.height);
					node_view.IsExpanded = EditorGUI.Foldout(rect_foldout, node_view.IsExpanded, GUIContent.none);
				}

				if(mIsCheckableView)
				{
					Rect rect_checked = new Rect(rect.x + 12, rect.y + 1, 12, rect.height);
					if (node_view.IsChecked.HasValue)
					{
						node_view.IsChecked = EditorGUI.ToggleLeft(rect_checked, GUIContent.none, node_view.IsChecked.Value);
					}
					else
					{
						EditorGUI.showMixedValue = true;
						EditorGUI.ToggleLeft(rect_checked, GUIContent.none, node_view.IsChecked.Value);
						EditorGUI.showMixedValue = false;
					}

					Rect rect_value = new Rect(rect.x + 26, rect.y, rect.width - 24, rect.height);
					EditorGUI.LabelField(rect_value, label_сontent, selected ? EditorStyles.whiteBoldLabel : EditorStyles.label);
				}
				else
				{
					Rect rect_value = new Rect(rect.x + 14, rect.y, rect.width - 14, rect.height);
					EditorGUI.LabelField(rect_value, label_сontent, selected ? EditorStyles.whiteBoldLabel : EditorStyles.label);
				}
			}
			#endregion
		}
	}
}
//=====================================================================================================================