//======================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Базовая подсистема Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//----------------------------------------------------------------------------------------------------------------------
/** \file LotusBaseUnityContainer.cs
*		Контейнер для создания, хранение и управление списком дочерних элементов.
*		Контейнер обеспечивает базовую функциональность для создания, хранения и управления списком дочерних элементов 
*	применительно к игровым объектам Unity. 
*		Он обеспечивает синхронизацию между позицией элемента в списке и позицией компонента трасформации применительно
*	к игровому объекту.
*/
//----------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//======================================================================================================================
using System;
using System.Collections;
using UnityEngine;
//======================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//--------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreUnityBase
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Контейнер для создания, хранение и управление списком дочерних элементов
		/// </summary>
		/// <remarks>
		/// <para>
		/// Контейнер обеспечивает базовую функциональность для создания, хранения и управления списком дочерних элементов 
		/// применительно к игровым объектам Unity
		/// </para>
		/// <para>
		/// Он обеспечивает синхронизацию между позицией элемента в списке и позицией компонента трасформации применительно 
		/// к игровому объекту
		/// </para>
		/// </remarks>
		/// <typeparam name="TItem">Тип элемента</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class ContainerComponent<TItem> : ListArray<TItem> where TItem : UnityEngine.Component
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			[SerializeField]
			internal Transform mParent;

			// Механизм создания элементов
			[NonSerialized]
			internal IList mItemsSource;
			[NonSerialized]
			internal Func<System.Object, TItem> mItemConstructor;
			[SerializeField]
			internal TItem mItemPrefab;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Родительский компонент трансформации
			/// </summary>
			public Transform Parent
			{
				get { return mParent; }
				set
				{
					mParent = value;
				}
			}

			//
			// МЕХАНИЗМ СОЗДАНИЯ ЭЛЕМЕНТОВ
			//
			/// <summary>
			/// Источник данных
			/// </summary>
			public IList ItemsSource
			{
				get { return (mItemsSource); }
				set
				{
					mItemsSource = value;
				}
			}

			/// <summary>
			/// Внешний конструктор для создания элемента
			/// </summary>
			public Func<System.Object, TItem> ItemConstructor
			{
				get { return mItemConstructor; }
				set { mItemConstructor = value; }
			}

			/// <summary>
			/// Префаб для создания элемента
			/// </summary>
			public TItem ItemPrefab
			{
				get { return mItemPrefab; }
				set { mItemPrefab = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные контейнера предустановленными данными
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public ContainerComponent()
				: base(INIT_MAX_COUNT)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные контейнера указанными данными
			/// </summary>
			/// <param name="max_count">Максимальное количество элементов</param>
			//---------------------------------------------------------------------------------------------------------
			public ContainerComponent(Int32 max_count)
				: base(max_count)
			{
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление индексов компонентов трансформации
			/// </summary>
			/// <remarks>
			/// Подразумевается что положение элементов в списке изменилось, например в результате сортировки или 
			/// перемещения элемента и теперь необходимо синхронизировать индексы компонента трансформации 
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public virtual void UpdateIndexTransformSibling()
			{
				for (Int32 i = 0; i < Count; i++)
				{
					mArrayOfItems[i].transform.SetSiblingIndex(i);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление индексов элементов
			/// </summary>
			/// <remarks>
			/// Подразумевается что индексы компонентов трансформации изменились теперь необходимо синхронизировать
			/// с положение данных элементов в списке
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public virtual void UpdateIndexItems()
			{

			}
			#endregion

			#region ======================================= МЕТОДЫ ДОБАВЛЕНИЯ ЭЛЕМЕНТОВ ===============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление существующего элемента в контейнер
			/// </summary>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void AddExistsItem(TItem item)
			{
				// Присваиваем в иерархию
				if (mParent != null)
				{
					item.transform.SetParent(mParent, false);
				}

				item.name = "Item_" + Count.ToString();

				// Добавляем в список
				Add(item);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание из префаба и добавление элемента в контейнер
			/// </summary>
			/// <returns>Созданный элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual TItem AddNewItemFromPrefab()
			{
#if UNITY_EDITOR
				if (mItemPrefab == null)
				{
					Debug.LogError("ItemPrefab == NULL");
					return default(TItem);
				}
#endif
				// Создаем элемент и добавляем
				TItem item = GameObject.Instantiate(mItemPrefab);
				item.gameObject.SetActive(true);
				AddExistsItem(item);
				return item;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание элемента из указанного префаба и добавление элемента в контейнер
			/// </summary>
			/// <param name="item_prefab">Префаб элемента</param>
			/// <returns>Созданный элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual TItem AddNewItemFromPrefab(TItem item_prefab)
			{
#if UNITY_EDITOR
				if (item_prefab == null)
				{
					Debug.LogError("item_prefab == NULL");
					return default(TItem);
				}
#endif
				// Создаем элемент и добавляем
				TItem item = GameObject.Instantiate(item_prefab);
				item.gameObject.SetActive(true);
				AddExistsItem(item);
				return item;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Дублирование элемента и добавление элемента в контейнер
			/// </summary>
			/// <param name="index">Индекс дублируемого элемента</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void DublicateItem(Int32 index)
			{
				// Создаем элемент
				TItem item = GameObject.Instantiate(this[index]);
				AddExistsItem(item);
			}
			#endregion

			#region ======================================= МЕТОДЫ ВСТАВКИ ЭЛЕМЕНТОВ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вставка элемента в указанную позицию
			/// </summary>
			/// <param name="index">Позиция вставки</param>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void InsertItem(Int32 index, TItem item)
			{
				// Присваиваем в иерархию
				if (mParent != null)
				{
					item.transform.SetParent(mParent, false);
				}

				item.name = "Item_" + Count.ToString();

				// Вставляем в список
				Insert(index, item);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вставка элемента после указанного элемента
			/// </summary>
			/// <param name="original">Элемент после которого будет произведена вставка</param>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void InsertAfterItem(TItem original, TItem item)
			{
				if (item == null) return;

				Int32 index = IndexOf(original);
				InsertItem(index + 1, item);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вставка элемента перед указанным элементом
			/// </summary>
			/// <param name="original">Элемент перед которым будет произведена вставка</param>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void InsertBeforeItem(TItem original, TItem item)
			{
				if (item == null) return;

				Int32 index = IndexOf(original);
				InsertItem(index, item);
			}
			#endregion

			#region ======================================= МЕТОДЫ УДАЛЕНИЯ ЭЛЕМЕНТОВ =================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента из контейнера по указанному имени игрового объекта
			/// </summary>
			/// <param name="game_object_name">Имя игрового объекта</param>
			/// <returns>Статус успешности удаления</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean RemoveItem(String game_object_name)
			{
				Boolean status = false;
				for (Int32 i = 0; i < Count; i++)
				{
					if (mArrayOfItems[i].gameObject.name == game_object_name)
					{
						status = RemoveItem(mArrayOfItems[i]);
						break;
					}
				}

				return (status);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента из контейнера по указанному имени игрового объекта вместе с игровым объектом
			/// </summary>
			/// <param name="game_object_name">Имя игрового объекта</param>
			/// <returns>Статус успешности удаления</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean DeleteItem(String game_object_name)
			{
				Boolean status = false;
				for (Int32 i = 0; i < Count; i++)
				{
					if (mArrayOfItems[i].gameObject.name == game_object_name)
					{
						XGameObjectDispatcher.Destroy(mArrayOfItems[i].gameObject);
						status = RemoveItem(mArrayOfItems[i]);
						break;
					}
				}

				return (status);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента из контейнера по индексу
			/// </summary>
			/// <param name="index">Индекс элемента</param>
			/// <returns>Статус успешности удаления</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean RemoveItem(Int32 index)
			{
				// Удаляем элемент
				RemoveAt(index);
				return (true);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента из контейнера по индексу вместе с игровым объектом
			/// </summary>
			/// <param name="index">Индекс элемента</param>
			/// <returns>Статус успешности удаления</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean DeleteItem(Int32 index)
			{
				// Удаляем элемент
				XGameObjectDispatcher.Destroy(mArrayOfItems[index].gameObject);
				RemoveAt(index);
				return (true);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента из контейнера
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Статус успешности удаления</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean RemoveItem(TItem item)
			{
				if (item == null) return false;

				// 2) Удаляем элемент
				if (Remove(item))
				{
					return (true);
				}

				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента из контейнера вместе с игровым объектом
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Статус успешности удаления</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean DeleteItem(TItem item)
			{
				if (item == null) return false;

				// 2) Удаляем элемент
				if (Remove(item))
				{
					XGameObjectDispatcher.Destroy(item.gameObject);
					return (true);
				}

				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление всех элементов из контейнера
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void ClearItems()
			{
				Clear();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление всех элементов из контейнера вместе с игровыми объектами
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void DeleteItems()
			{
				for (Int32 i = 0; i < Count; i++)
				{
					XGameObjectDispatcher.Destroy(mArrayOfItems[i].gameObject);
				}

				Clear();
			}
			#endregion

			#region ======================================= МЕТОДЫ ПОИСКА ЭЛЕМЕНТОВ ===================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на существовании элемента в контейнере
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Статус существования элемента</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean ExistsItem(TItem item)
			{
				if (item == null) return (false);

				for (Int32 i = 0; i < Count; i++)
				{
					if (mArrayOfItems[i].Equals(item))
					{
						return (true);
					}
				}

				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск индекса указанного элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Индекс элемента или -1 если модель не найдена</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Int32 IndexOfItem(TItem item)
			{
				return (IndexOf(item));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск элемента по указанному имени игрового объекта
			/// </summary>
			/// <param name="game_object_name">Имя игрового объекта</param>
			/// <returns>Найденный модель или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual TItem GetItemFromName(String game_object_name)
			{
				for (Int32 i = 0; i < Count; i++)
				{
					if (mArrayOfItems[i].gameObject.name == game_object_name)
					{
						return (mArrayOfItems[i]);
					}
				}

				return (default(TItem));
			}
			#endregion

			#region ======================================= МЕТОДЫ ПЕРЕМЕЩЕНИЯ ЭЛЕМЕНТОВ ==============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перемещение элемента в новую позицию
			/// </summary>
			/// <param name="old_index">Старая позиция</param>
			/// <param name="new_index">Новая позиция</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void MoveItem(Int32 old_index, Int32 new_index)
			{
				Move(old_index, new_index);

				if (mParent != null)
				{
					Transform transform = mParent.GetChild(old_index);
					if (transform != null)
					{
						transform.SetSiblingIndex(new_index);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перемещение элемента вверх по списку
			/// </summary>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void MoveUpItem(TItem item)
			{
				if (item == null) return;

				Int32 index = IndexOf(item);
				if (index > 0)
				{
					MoveUp(index);

					if (mParent != null)
					{
						Transform transform = mParent.GetChild(index);
						if (transform != null)
						{
							transform.SetSiblingIndex(index - 1);
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перемещение элемента вниз по списку
			/// </summary>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void MoveDownItem(TItem item)
			{
				if (item == null) return;

				Int32 index = IndexOf(item);
				if (index > -1 && index < Count)
				{
					MoveDown(index);

					if (mParent != null)
					{
						Transform transform = mParent.GetChild(index) as Transform;
						if (transform != null)
						{
							transform.SetSiblingIndex(index + 1);
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сортировка элементов по возрастанию
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void SortItemsAscending()
			{
				SortAscending();
				UpdateIndexTransformSibling();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сортировка элементов по убыванию
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void SortItemsDescending()
			{
				SortDescending();
				UpdateIndexTransformSibling();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сортировка элементов посредством делегата
			/// </summary>
			/// <param name="comparison">Делегат сравнивающий два объекта одного типа</param>
			//---------------------------------------------------------------------------------------------------------
			public void SortItems(Comparison<TItem> comparison)
			{
				Sort(comparison);
				UpdateIndexTransformSibling();
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Контейнер для компонента <see cref="RectTransform"/>
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class CContainerRectTransform : ContainerComponent<RectTransform>
		{
			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные контейнера предустановленными данными
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CContainerRectTransform()
				: base(INIT_MAX_COUNT)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные контейнера указанными данными
			/// </summary>
			/// <param name="max_count">Максимальное количество элементов</param>
			//---------------------------------------------------------------------------------------------------------
			public CContainerRectTransform(Int32 max_count)
				: base(max_count)
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