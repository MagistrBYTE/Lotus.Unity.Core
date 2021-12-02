﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема файловой системы
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusFileSystemView.cs
*		Специализация элементов отображения для работы с объектам файловой системы.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 04.04.2021
//=====================================================================================================================
using System;
using System.IO;
using System.Linq;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreFileSystem
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс реализующий элемент отображения для элемента файловой системы
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CViewItemHierarchyFileSystem : ViewItemHierarchy<ILotusFileSystemEntity>
		{
			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CViewItemHierarchyFileSystem()
				: this(String.Empty)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя модели</param>
			//---------------------------------------------------------------------------------------------------------
			public CViewItemHierarchyFileSystem(String name)
				: base(name)
			{
				SetContextMenu();
				mIsNotify = true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="data_context">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public CViewItemHierarchyFileSystem(ILotusFileSystemEntity data_context)
				: base(data_context)
			{
				SetContextMenu();
				mIsNotify = true;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="data_context">Данные</param>
			/// <param name="parent_item">Родительский узел</param>
			//---------------------------------------------------------------------------------------------------------
			public CViewItemHierarchyFileSystem(ILotusFileSystemEntity data_context, ILotusViewItemHierarchy parent_item)
				: base(data_context, parent_item)
			{
				SetContextMenu();
				mIsNotify = true;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка контекстного меню
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void SetContextMenu()
			{
				mUIContextMenu = new CUIContextMenu();
				mUIContextMenu.ViewItem = this;
				mUIContextMenu.AddItem("Показать в проводнике", (ILotusViewItem view_item) =>
				{
#if USE_WINDOWS
					Windows.XNative.ShellExecute(IntPtr.Zero,
						"explore",
						DataContext.FullName,
						"",
						"",
						Windows.TShowCommands.SW_NORMAL);
#else
					
#endif
				});
				mUIContextMenu.AddItem(CUIContextMenu.Remove.Duplicate());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Открытие контекстного меню
			/// </summary>
			/// <param name="context_menu">Контекстное меню</param>
			//---------------------------------------------------------------------------------------------------------
			public override void OpenContextMenu(System.Object context_menu)
			{
#if USE_WINDOWS
				if(context_menu is System.Windows.Controls.ContextMenu window_context_menu)
				{
					mUIContextMenu.SetCommandsDefault(window_context_menu);
				}
#endif
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Построение дочерней иерархии согласно источнику данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public override void BuildFromDataContext()
			{
				Clear();
				CCollectionViewHierarchyFileSystem.BuildFromParent(this, mOwner);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Коллекция для отображения элементов файлов системы
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CCollectionViewHierarchyFileSystem : CollectionViewHierarchy<CViewItemHierarchyFileSystem, ILotusFileSystemEntity>
		{
			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CCollectionViewHierarchyFileSystem()
				: base(String.Empty)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя коллекции</param>
			//---------------------------------------------------------------------------------------------------------
			public CCollectionViewHierarchyFileSystem(String name)
				: base(name)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя коллекции</param>
			/// <param name="source">Источник данных</param>
			//---------------------------------------------------------------------------------------------------------
			public CCollectionViewHierarchyFileSystem(String name, ILotusFileSystemEntity source)
				: base(name, source)
			{
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================