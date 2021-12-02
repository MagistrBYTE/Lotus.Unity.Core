﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusAssetsBrowserWindow.cs
*		Редактор обозреватель ресурсов проекта.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 04.04.2021
//=====================================================================================================================
#if UNITY_EDITOR
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
using Lotus.Editor;
//=====================================================================================================================
//---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Редактор обозреватель ресурсов проекта
/// </summary>
//---------------------------------------------------------------------------------------------------------------------
public class LotusAssetsBrowserWindow : LotusUtilityBaseWindow
{
	#region =============================================== СТАТИЧЕСКИЕ ДАННЫЕ ========================================
	protected static readonly GUIContent mContentDirectory = new GUIContent("Директория:");
	protected static readonly GUIContent mContentDirectoryValue = new GUIContent("");
	protected static readonly GUIContent mContentFile = new GUIContent("Файл:");
	protected static readonly GUIContent mContentFileValue = new GUIContent("");
	#endregion

	#region =============================================== СТАТИЧЕСКИЕ МЕТОДЫ ========================================
	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Показать окно для просмотра, масштабирования и панорамирование внутреннего контента
	/// </summary>
	//-----------------------------------------------------------------------------------------------------------------
	[MenuItem(XCoreEditorSettings.MenuPathUtility + "Assets Browser", false, XCoreEditorSettings.MenuOrderUtility + 20)]
	public static void ShowAssetsBrowser()
	{
		var window = GetWindow<LotusAssetsBrowserWindow>();
		window.titleContent.text = "Assets Browser";

		window.OnConstruct();
		window.Show();
	}
	#endregion

	#region =============================================== ДАННЫЕ ====================================================
	// Основные данные
	[SerializeField]
	protected CTreeViewControl mExploreFile;
	protected SearchField mSearchField;
	protected CFileSystemDirectory mBaseDirectory;
	protected Editor mEditorAsset;
	protected String mRemoveStartName;
	protected String mRemoveEndName;
	protected String mReplaceEndNameSource;
	protected String mReplaceEndNameTarget;
	#endregion

	#region =============================================== СОБЫТИЯ UNITY =============================================
	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Включение утилиты
	/// </summary>
	//-----------------------------------------------------------------------------------------------------------------
	public void OnEnable()
	{
		mBaseDirectory = new CFileSystemDirectory(XEditorSettings.ASSETS_PATH);
		mBaseDirectory.RecursiveFileSystemInfo();
		mSearchField = new SearchField();
		mExploreFile = new CTreeViewControl(mBaseDirectory);
		mExploreFile.IsCheckableView = true;
	}

	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Отключение утилиты
	/// </summary>
	//-----------------------------------------------------------------------------------------------------------------
	public void OnDisable()
	{
		
	}
	#endregion

	#region =============================================== ОБШИЕ МЕТОДЫ ==============================================
	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Конструирование окна
	/// </summary>
	//-----------------------------------------------------------------------------------------------------------------
	public void OnConstruct()
	{
		mIsManager = false;
	}
	#endregion

	#region =============================================== МЕТОДЫ РИСОВАНИЯ ==========================================
	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Рисование панели обозревателя
	/// </summary>
	//-----------------------------------------------------------------------------------------------------------------
	public override void DrawExplore()
	{
		GUILayout.BeginHorizontal(EditorStyles.toolbar);
		{
			mExploreFile.SearchString = mSearchField.OnToolbarGUI(mExploreFile.SearchString);
		}
		GUILayout.EndHorizontal();

		GUILayout.Space(2.0f);
		GUILayout.BeginVertical();
		{
			mExploreFile.DrawTreeLayout();
		}
		GUILayout.EndVertical();
	}

	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Рисование основной панели
	/// </summary>
	//-----------------------------------------------------------------------------------------------------------------
	public override void DrawContent()
	{
		GUILayout.Space(4);
		GUILayout.Label("Основные действия с выбранными элементами(файлами)", EditorStyles.boldLabel);

		GUILayout.Space(8);
		GUILayout.BeginHorizontal();
		{
			mRemoveStartName = EditorGUILayout.TextField("Удалить с начала", mRemoveStartName);
			GUILayout.Space(2);
			if (GUILayout.Button("Удалить"))
			{
				if (mRemoveStartName.IsExists())
				{
					//mExploreFile.Root.VisitDataChecked((ILotusModel node) =>
					//{
					//	CFileSystemFile file_node = node as CFileSystemFile;
					//	if (file_node != null)
					//	{
					//		file_node.ModifyNameOfRemove(TStringSearchOption.Start, mRemoveStartName);
					//	}
					//});
				}
			}
		}
		GUILayout.EndHorizontal();

		GUILayout.Space(8);
		GUILayout.BeginHorizontal();
		{
			mRemoveEndName = EditorGUILayout.TextField("Удалить с конца", mRemoveEndName);
			GUILayout.Space(2);
			if (GUILayout.Button("Удалить"))
			{
				if (mRemoveEndName.IsExists())
				{
					//mExploreFile.Root.VisitDataChecked((ILotusModel node) =>
					//{
					//	CFileSystemFile file_node = node as CFileSystemFile;
					//	if (file_node != null)
					//	{
					//		file_node.ModifyNameOfRemove(TStringSearchOption.End, mRemoveEndName);
					//	}
					//});
				}
			}
		}
		GUILayout.EndHorizontal();

		GUILayout.Space(8);
		GUILayout.BeginHorizontal();
		{
			mReplaceEndNameSource = EditorGUILayout.TextField("Заменить с конца", mReplaceEndNameSource);
			mReplaceEndNameTarget = EditorGUILayout.TextField("на", mReplaceEndNameTarget);

			GUILayout.Space(2);
			if (GUILayout.Button("Заменить"))
			{
				//if (mReplaceEndNameSource.IsExists() && mReplaceEndNameTarget.IsExists())
				//{
				//	Int32 count = mExploreFile.Root.GetCountCheckedNodes();
				//	Int32 index = 0;
				//	mExploreFile.Root.VisitDataChecked((ILotusModel node) =>
				//	{
				//		CFileSystemFile file_node = node as CFileSystemFile;
				//		if (file_node != null)
				//		{
				//			file_node.ModifyNameOfReplace(TStringSearchOption.Start, mReplaceEndNameSource,
				//				mReplaceEndNameTarget);

				//			EditorUtility.DisplayCancelableProgressBar("Заменить", file_node.Name, 
				//				(Single)(index + 1) / (Single)count);

				//			index++;
				//		}
				//	});

				//	EditorUtility.ClearProgressBar();
				//}
			}
		}
		GUILayout.EndHorizontal();

		GUILayout.Space(2);
	}
	#endregion
}
//=====================================================================================================================
#endif
//=====================================================================================================================