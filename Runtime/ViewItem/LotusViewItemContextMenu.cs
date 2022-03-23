//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема отображения данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusViewItemContextMenu.cs
*		Определение кроссплатформенного контекстного меню.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
//---------------------------------------------------------------------------------------------------------------------
#if USE_WINDOWS
using Lotus.Windows;
#endif
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreViewItem
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс инкапсулирующий элемент контекстного меню
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CUIContextMenuItem : ILotusDuplicate<CUIContextMenuItem>
		{
			#region ======================================= ДАННЫЕ ====================================================
			public ILotusViewItem ViewItem;
			public Action<ILotusViewItem> OnAction;
			public Action<ILotusViewItem> OnAfterAction;
#if USE_WINDOWS
			public System.Windows.Controls.MenuItem MenuItem;
#endif
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="view_item">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem(ILotusViewItem view_item)
				: this(view_item, String.Empty, null, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="view_item">Элемент отображения</param>
			/// <param name="name">Имя элемента меню</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem(ILotusViewItem view_item, String name)
				: this(view_item, name, null, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя элемента меню</param>
			/// <param name="on_action">Обработчик событие основного действия</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem(String name, Action<ILotusViewItem> on_action)
				: this(null, name, on_action, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="view_item">Элемент отображения</param>
			/// <param name="name">Имя элемента меню</param>
			/// <param name="on_action">Обработчик событие основного действия</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem(ILotusViewItem view_item, String name, Action<ILotusViewItem> on_action)
				: this(view_item, name, on_action, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="view_item">Элемент отображения</param>
			/// <param name="name">Имя элемента меню</param>
			/// <param name="on_action">Обработчик событие основного действия</param>
			/// <param name="on_after_action">Дополнительный обработчик события после основного действия</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem(ILotusViewItem view_item, String name, Action<ILotusViewItem> on_action, 
				Action<ILotusViewItem> on_after_action)
			{
				ViewItem = view_item;
				OnAction = on_action;
				OnAfterAction = on_after_action;

#if USE_WINDOWS
				CreateMenuItem(name, null);
#endif
			}

#if USE_WINDOWS
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя элемента меню</param>
			/// <param name="on_action">Обработчик событие основного действия</param>
			/// <param name="icon">Графическая иконка</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem(String name, Action<ILotusViewItem> on_action, System.Drawing.Bitmap icon)
			{
				OnAction = on_action;
				CreateMenuItem(name, icon);
			}
#endif
			#endregion

			#region ======================================= МЕТОДЫ ILotusDuplicate ====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение дубликата объекта
			/// </summary>
			/// <returns>Дубликат объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenuItem Duplicate()
			{
				CUIContextMenuItem item = new CUIContextMenuItem();
				item.ViewItem = ViewItem;
				item.OnAction = OnAction;
				item.OnAfterAction = OnAfterAction;
#if USE_WINDOWS
				item.CreateMenuItem(MenuItem);
#endif
				return (item);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
#if USE_WINDOWS
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание элемента меню
			/// </summary>
			/// <param name="name">Имя элемента меню</param>
			/// <param name="icon">Графическая иконка</param>
			//---------------------------------------------------------------------------------------------------------
			public void CreateMenuItem(String name, System.Drawing.Bitmap icon)
			{
				if (MenuItem == null)
				{
					MenuItem = new System.Windows.Controls.MenuItem();
					MenuItem.Header = name;
					MenuItem.Click += OnItemClick;
					if (icon != null)
					{
						MenuItem.Icon = new System.Windows.Controls.Image
						{
							Source = icon.ToBitmapSource(),
							Width = 16,
							Height = 16
						};
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание элемента меню
			/// </summary>
			/// <param name="menu_item">Элемента меню</param>
			//---------------------------------------------------------------------------------------------------------
			public void CreateMenuItem(System.Windows.Controls.MenuItem menu_item)
			{
				if (MenuItem == null)
				{
					MenuItem = new System.Windows.Controls.MenuItem();
					MenuItem.Header = menu_item.Header;
					MenuItem.Icon = menu_item.Icon;
					MenuItem.Click += OnItemClick;
				}
				else
				{
					MenuItem.Header = menu_item.Header;
					MenuItem.Icon = menu_item.Icon;
				}
			}
#endif
			#endregion

			#region ======================================= ОБРАБОТЧИКИ СОБЫТИЙ =======================================
#if USE_WINDOWS
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка события щелчка на элементе меню
			/// </summary>
			/// <param name="sender">Источник события</param>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			private void OnItemClick(Object sender, System.Windows.RoutedEventArgs args)
			{
				if(OnAction != null)
				{
					OnAction(ViewItem);
				}
				if (OnAfterAction != null)
				{
					OnAfterAction(ViewItem);
				}
			}
#endif
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс инкапсулирующий контекстное меню
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CUIContextMenu
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
#if USE_WINDOWS
			/// <summary>
			/// Элемент меню - загрузить объект из файла
			/// </summary>
			public readonly static CUIContextMenuItem Load = new CUIContextMenuItem("Загрузить...", 
				OnLoadItemClick, XResources.Oxygen_document_open_32);

			/// <summary>
			/// Элемент меню - сохранить объект в файл
			/// </summary>
			public readonly static CUIContextMenuItem Save = new CUIContextMenuItem("Сохранить...", 
				OnSaveItemClick, XResources.Oxygen_document_save_32);

			/// <summary>
			/// Элемент меню - удалить объект
			/// </summary>
			public readonly static CUIContextMenuItem Remove = new CUIContextMenuItem("Удалить", 
				OnRemoveItemClick, XResources.Oxygen_list_remove_32);

			/// <summary>
			/// Элемент меню - дублировать объект
			/// </summary>
			public readonly static CUIContextMenuItem Duplicate = new CUIContextMenuItem("Дублировать", 
				OnDuplicateItemClick, XResources.Oxygen_tab_duplicate_32);

			/// <summary>
			/// Элемент меню - переместить объект вверх
			/// </summary>
			public readonly static CUIContextMenuItem MoveUp = new CUIContextMenuItem("Переместить вверх", 
				OnMoveUpItemClick, XResources.Oxygen_arrow_up_22);

			/// <summary>
			/// Элемент меню - переместить объект вниз
			/// </summary>
			public readonly static CUIContextMenuItem MoveDown = new CUIContextMenuItem("Переместить вниз", 
				OnMoveDownItemClick, XResources.Oxygen_arrow_down_22);

			/// <summary>
			/// Элемент меню - не учитывать объект в расчетах
			/// </summary>
			public readonly static CUIContextMenuItem NotCalculation = new CUIContextMenuItem("Не учитывать в расчетах",
				OnNotCalculationItemClick, XResources.Oxygen_user_busy_32);
#else
			/// <summary>
			/// Элемент меню - загрузить объект из файла
			/// </summary>
			public readonly static CUIContextMenuItem Load = new CUIContextMenuItem("Загрузить...", OnLoadItemClick);

			/// <summary>
			/// Элемент меню - сохранить объект в файл
			/// </summary>
			public readonly static CUIContextMenuItem Save = new CUIContextMenuItem("Сохранить...", OnSaveItemClick);

			/// <summary>
			/// Элемент меню - удалить объект
			/// </summary>
			public readonly static CUIContextMenuItem Remove = new CUIContextMenuItem("Удалить", OnRemoveItemClick);

			/// <summary>
			/// Элемент меню - дублировать объект
			/// </summary>
			public readonly static CUIContextMenuItem Duplicate = new CUIContextMenuItem("Дублировать", OnDuplicateItemClick);

			/// <summary>
			/// Элемент меню - переместить объект вверх
			/// </summary>
			public readonly static CUIContextMenuItem MoveUp = new CUIContextMenuItem("Переместить вверх", OnMoveUpItemClick);

			/// <summary>
			/// Элемент меню - переместить объект вниз
			/// </summary>
			public readonly static CUIContextMenuItem MoveDown = new CUIContextMenuItem("Переместить вниз", OnMoveDownItemClick);

			/// <summary>
			/// Элемент меню - не учитывать объект в расчетах
			/// </summary>
			public readonly static CUIContextMenuItem NotCalculation = new CUIContextMenuItem("Не учитывать в расчетах", OnNotCalculationItemClick);
#endif
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка события загрузка объекта из файла
			/// </summary>
			/// <param name="view_item">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			private static void OnLoadItemClick(ILotusViewItem view_item)
			{
				if (view_item != null && view_item.DataContext != null)
				{
					String file_name = XFileDialog.Open();
					if (file_name.IsExists())
					{
						// Уведомляем о начале загрузки
						if (view_item.DataContext is ILotusBeforeLoad before_load)
						{
							before_load.OnBeforeLoad(null);
						}

						if (view_item.DataContext is ILotusOwnerObject owner_object)
						{
							owner_object.UpdateOwnedObjects();
						}

						// Уведомляем об окончании загрузки
						if (view_item.DataContext is ILotusAfterLoad after_load)
						{
							after_load.OnAfterLoad(null);
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка события сохранения объекта в файл
			/// </summary>
			/// <param name="view_item">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			private static void OnSaveItemClick(ILotusViewItem view_item)
			{
				if (view_item != null && view_item.DataContext != null)
				{
					String file_name = XFileDialog.Save();
					if (file_name.IsExists())
					{
						// Уведомляем о начале сохранения 
						if (view_item.DataContext is ILotusBeforeSave before_save)
						{
							before_save.OnBeforeSave(null);
						}

						//XSerializationDispatcher.SaveTo(file_name, view_item.DataContext);

						// Уведомляем об окончании сохранения 
						if (view_item.DataContext is ILotusAfterSave after_save)
						{
							after_save.OnAfterSave(null);
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка события удаления объекта
			/// </summary>
			/// <param name="view_item">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			private static void OnRemoveItemClick(ILotusViewItem view_item)
			{
				// Удаляем с отображения
				if(view_item.IOwner is ILotusOwnerObject owner)
				{
					owner.DetachOwnedObject(view_item, true);
				}
				if (view_item.IOwner is IList list)
				{
					if(list.IndexOf(view_item) != -1)
					{
						list.Remove(view_item);
					}
				}

				// Удаляем реальные данные
				if (view_item.DataContext is ILotusOwnerObject owner_data_context)
				{
					owner_data_context.DetachOwnedObject(view_item.DataContext as ILotusOwnedObject, true);
				}
				else
				{
					//if (view_item.DataContext is IList data_context)
					//{
					//	list.Remove(view_item.DataContext);
					//}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка события дублирование объекта
			/// </summary>
			/// <param name="view_item">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			private static void OnDuplicateItemClick(ILotusViewItem view_item)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка события перемещение объекта вверх
			/// </summary>
			/// <param name="view_item">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			private static void OnMoveUpItemClick(ILotusViewItem view_item)
			{
				if (view_item != null && view_item.IOwner is IList list)
				{
					Int32 index = list.IndexOf(view_item);
					if(index > 0)
					{
						list.MoveObjectUp(index);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка события перемещение объекта вниз
			/// </summary>
			/// <param name="view_item">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			private static void OnMoveDownItemClick(ILotusViewItem view_item)
			{
				if (view_item != null && view_item.IOwner is IList list)
				{
					Int32 index = list.IndexOf(view_item);
					if (index > -1 && index < list.Count - 1)
					{
						list.MoveObjectDown(index);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка события смены статуса объекта для учитывания в расчетах
			/// </summary>
			/// <param name="view_item">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			private static void OnNotCalculationItemClick(ILotusViewItem view_item)
			{
				if (view_item != null && view_item.DataContext is ILotusNotCalculation calculation)
				{
					calculation.NotCalculation = !calculation.NotCalculation;
				}
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			public ILotusViewItem ViewItem;
			public Boolean IsCreatedItems;
			public List<CUIContextMenuItem> Items;
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenu()
				: this(null, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="view_item">Элемент отображения</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenu(ILotusViewItem view_item)
				: this(view_item, null)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="view_item">Элемент отображения</param>
			/// <param name="items">Набор элементов меню</param>
			//---------------------------------------------------------------------------------------------------------
			public CUIContextMenu(ILotusViewItem view_item, params CUIContextMenuItem[] items)
			{
				ViewItem = view_item;
				if (items != null && items.Length > 0)
				{
					Items = new List<CUIContextMenuItem>(items);
				}
				else
				{
					Items = new List<CUIContextMenuItem>();
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элемента меню
			/// </summary>
			/// <param name="menu_item">Элемент меню</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddItem(CUIContextMenuItem menu_item)
			{
				Items.Add(menu_item);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элементов меню
			/// </summary>
			/// <param name="items">Набор элементов меню</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddItem(params CUIContextMenuItem[] items)
			{
				Items.AddRange(items);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элемента меню
			/// </summary>
			/// <param name="name">Имя элемента меню</param>
			/// <param name="on_action">Обработчик событие основного действия</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddItem(String name, Action<ILotusViewItem> on_action)
			{
				Items.Add(new CUIContextMenuItem(name, on_action));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элемента меню
			/// </summary>
			/// <param name="name">Имя элемента меню</param>
			/// <param name="on_action">Обработчик событие основного действия</param>
			/// <param name="on_after_action">Дополнительный обработчик события после основного действия</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddItem(String name, Action<ILotusViewItem> on_action, Action<ILotusViewItem> on_after_action)
			{
				Items.Add(new CUIContextMenuItem(null, name, on_action, on_after_action));
			}

#if USE_WINDOWS
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элемента меню
			/// </summary>
			/// <param name="name">Имя элемента меню</param>
			/// <param name="on_action">Обработчик события элемента меню</param>
			/// <param name="icon">Иконка элемента меню</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddItem(String name, Action<ILotusViewItem> on_action, System.Drawing.Bitmap icon)
			{
				Items.Add(new CUIContextMenuItem(name, on_action, icon));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка команд для контекстного меню по умолчанию
			/// </summary>
			/// <param name="context_menu">Контекстное меню</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetCommandsDefault(System.Windows.Controls.ContextMenu context_menu)
			{
				if (ViewItem == null) return;

				// Устанавливаем/обновляем модель
				if (IsCreatedItems == false)
				{
					for (Int32 i = 0; i < Items.Count; i++)
					{
						// Если у экземпляра меню есть уже родитель то удалям
						if (Items[i].MenuItem.Items != null)
						{
							System.Windows.Controls.ItemCollection item_collection = Items[i].MenuItem.Items;
							item_collection.Remove(Items[i].MenuItem);
						}

						Items[i].ViewItem = ViewItem;
						context_menu.Items.Add(Items[i].MenuItem);
					}

					IsCreatedItems = true;
				}
				else
				{
					// Устанавливаем/обновляем модель
					for (Int32 i = 0; i < Items.Count; i++)
					{
						Items[i].ViewItem = ViewItem;
					}
				}
			}
#endif
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================