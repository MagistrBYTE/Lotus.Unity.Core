﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема коллекций
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusCollectionListArray.cs
*		Список на основе массива.
*		Реализация списка на основе массива. Данный список является базовыми типом коллекции для реализации других типов
*	коллекции.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreCollections
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Список на основе массива
		/// </summary>
		/// <remarks>
		/// Среди основных преимуществ использования собственного типа списка можно выделить:
		/// - Обеспечивается более высокая скорость работы за счет отказа проверок границ списка.
		/// - Можно получить доступ непосредственно к родному массиву.
		/// - Также поддерживается сериализации на уровни Unity, с учетом собственного редактора.
		/// - Поддержка уведомлений о смене состояний коллекции.
		/// - Поддержка обобщенного интерфейса IList.
		/// </remarks>
		/// <typeparam name="TItem">Тип элемента списка</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class ListArray<TItem> : IList<TItem>, IList, ILotusCheckOne<TItem>,
			ILotusCheckAll<TItem>, ILotusVisit<TItem>, INotifyPropertyChanged, INotifyCollectionChanged
		{
			#region ======================================= ВНУТРЕННИЕ ТИПЫ ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Тип реализующий перечислителя по списку
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[Serializable]
			public struct ListArrayEnumerator : IEnumerator<TItem>, IEnumerator
			{
				#region ======================================= ДАННЫЕ ================================================
				private ListArray<TItem> mList;
				private Int32 mIndex;
				private TItem mCurrent;
				#endregion

				#region ======================================= СВОЙСТВА ==============================================
				/// <summary>
				/// Текущий элемент
				/// </summary>
				public TItem Current
				{
					get
					{
						return mCurrent;
					}
				}

				/// <summary>
				/// Текущий элемент
				/// </summary>
				Object IEnumerator.Current
				{
					get
					{
						return Current;
					}
				}
				#endregion

				#region ======================================= КОНСТРУКТОРЫ ==========================================
				//-----------------------------------------------------------------------------------------------------
				/// <summary>
				/// Конструктор инициализирует данные перечислителя указанным списком
				/// </summary>
				/// <param name="list">Список</param>
				//-----------------------------------------------------------------------------------------------------
				internal ListArrayEnumerator(ListArray<TItem> list)
				{
					mList = list;
					mIndex = 0;
					mCurrent = default(TItem);
				}
				#endregion

				#region ======================================= ОБЩИЕ МЕТОДЫ ==========================================
				//-----------------------------------------------------------------------------------------------------
				/// <summary>
				/// Освобождение управляемых ресурсов
				/// </summary>
				//-----------------------------------------------------------------------------------------------------
				public void Dispose()
				{
				}

				//-----------------------------------------------------------------------------------------------------
				/// <summary>
				/// Переход к следующему элементу списка
				/// </summary>
				/// <returns>Возможность перехода к следующему элементу списка</returns>
				//-----------------------------------------------------------------------------------------------------
				public Boolean MoveNext()
				{
					if (mIndex < mList.Count)
					{
						mCurrent = mList.mArrayOfItems[mIndex];
						mIndex++;
						return (true);
					}
					else
					{
						mIndex = mList.Count + 1;
						mCurrent = default(TItem);
						return (false);
					}
				}

				//-----------------------------------------------------------------------------------------------------
				/// <summary>
				/// Перестановка позиции на первый элемент списка
				/// </summary>
				//-----------------------------------------------------------------------------------------------------
				void IEnumerator.Reset()
				{
					mIndex = 0;
					mCurrent = default(TItem);
				}
				#endregion
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный функтор для сравнения элемента
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			internal sealed class FunctorComparer : IComparer<TItem>
			{
				Comparison<TItem> Comparison;

				public FunctorComparer(Comparison<TItem> comparison)
				{
					this.Comparison = comparison;
				}

				public Int32 Compare(TItem x, TItem y)
				{
					return Comparison(x, y);
				}
			}
			#endregion

			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Максимальное количество элементов на начальном этапе
			/// </summary>
			public const Int32 INIT_MAX_COUNT = 8;

			/// <summary>
			/// Статус ссылочного типа элемента коллекции
			/// </summary>
			public static readonly Boolean IsNullable = !typeof(TItem).IsValueType || Nullable.GetUnderlyingType(typeof(TItem)) != null;

			/// <summary>
			/// Статус поддержки типом элемента интерфейса <see cref="ILotusIndexable"/>
			/// </summary>
			public static readonly Boolean IsIndexable = typeof(TItem).IsSupportInterface<ILotusIndexable>();

			/// <summary>
			/// Статус поддержки типом элемента интерфейса <see cref="ILotusDuplicate<TItem>"/>
			/// </summary>
			public static readonly Boolean IsDuplicatable = typeof(TItem).IsSupportInterface<ILotusDuplicate<TItem>>();

			/// <summary>
			/// Компаратор поддержки операций сравнения объектов в отношении равенства
			/// </summary>
			public static readonly EqualityComparer<TItem> EqualityComparerDefault = EqualityComparer<TItem>.Default;

			/// <summary>
			/// Компаратор поддержки операций сравнения объектов при упорядочении
			/// </summary>
			public static readonly Comparer<TItem> ComparerDefault = Comparer<TItem>.Default;
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			protected static readonly PropertyChangedEventArgs PropertyArgsCount = new PropertyChangedEventArgs(nameof(Count));
			protected static readonly PropertyChangedEventArgs PropertyArgsIndexer = new PropertyChangedEventArgs("Item[]");
			protected static readonly PropertyChangedEventArgs PropertyArgsIsEmpty = new PropertyChangedEventArgs(nameof(IsEmpty));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsFill = new PropertyChangedEventArgs(nameof(IsFill));
			protected static readonly NotifyCollectionChangedEventArgs CollectionArgsReset = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
#if (UNITY_2017_1_OR_NEWER)
			[UnityEngine.SerializeField]
#endif
			protected internal TItem[] mArrayOfItems;

#if (UNITY_2017_1_OR_NEWER)
			[UnityEngine.HideInInspector]
			[UnityEngine.SerializeField]
#endif
			protected internal Int32 mCount;

#if (UNITY_2017_1_OR_NEWER)
			[UnityEngine.HideInInspector]
			[UnityEngine.SerializeField]
#endif
			protected internal Int32 mMaxCount;

#if (UNITY_2017_1_OR_NEWER)
			[UnityEngine.HideInInspector]
			[UnityEngine.SerializeField]
#endif
			protected internal Boolean mIsNotify;
			protected internal Boolean mIsReadOnly;
			protected internal Boolean mIsFixedSize;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Максимальное количество элементов
			/// </summary>
			/// <remarks>
			/// Максимальное количество элементов на данном этапе, если текущее количество элементов будет равно максимальному,
			/// то при следующем добавления элемента в коллекцию произойдет перераспределения памяти и максимальное количество
			/// элементов увеличится в двое.
			/// Можно заранее увеличить максимальное количество элементов вызвав метод <see cref="Resize(Int32)"/>
			/// </remarks>
			public Int32 MaxCount
			{
				get { return mMaxCount;}
			}

			/// <summary>
			/// Статус пустой коллекции
			/// </summary>
			public Boolean IsEmpty
			{
				get { return mCount == 0; }
			}

			/// <summary>
			/// Статус заполненной коллекции
			/// </summary>
			/// <remarks>
			/// Статус заполненной коллекции означает то на текущий момент текущее количество элементов равно максимальному 
			/// и при следующем добавления элемента в коллекцию произойдет перераспределения памяти и максимальное количество 
			/// элементов увеличится в двое
			/// </remarks>
			public Boolean IsFill
			{
				get { return mCount == mMaxCount; }
			}

			/// <summary>
			/// Статус включения уведомлений коллекции о своих изменениях
			/// </summary>
			public Boolean IsNotify
			{
				get { return (mIsNotify); }
				set
				{
					mIsNotify = value;
				}
			}

			/// <summary>
			/// Индекс последнего элемента
			/// </summary>
			public Int32 LastIndex
			{
				get { return (mCount - 1); }
			}

			/// <summary>
			/// Данные массива для сериализации
			/// </summary>
			[Browsable(false)]
			public TItem[] SerializeItems
			{
				get 
				{
					TrimExcess();
					return (mArrayOfItems); 
				}
				set
				{
					SetData(value, value.Length);
				}
			}

			//
			// ДОСТУП К ЭЛЕМЕНТАМ
			//
			/// <summary>
			/// Первый элемент
			/// </summary>
			[Browsable(false)]
			public TItem ItemFirst
			{
				get
				{
					return (mArrayOfItems[0]);
				}
				set
				{
					mArrayOfItems[0] = value;
				}
			}

			/// <summary>
			/// Второй элемент
			/// </summary>
			[Browsable(false)]
			public TItem ItemSecond
			{
				get
				{
					return (mArrayOfItems[0]);
				}
				set
				{
					mArrayOfItems[0] = value;
				}
			}

			/// <summary>
			/// Предпоследний элемент
			/// </summary>
			[Browsable(false)]
			public TItem ItemPenultimate
			{
				get
				{
					return (mArrayOfItems[mCount - 2]);
				}
				set
				{
					mArrayOfItems[mCount - 2] = value;
				}
			}

			/// <summary>
			/// Последний элемент
			/// </summary>
			[Browsable(false)]
			public TItem ItemLast
			{
				get
				{
					return (mArrayOfItems[mCount - 1]);
				}
				set
				{
					mArrayOfItems[mCount - 1] = value;
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА IList ============================================
			/// <summary>
			/// Количество элементов
			/// </summary>
			public Int32 Count
			{
				get { return mCount; }
			}

			/// <summary>
			/// Статус коллекции только для чтения
			/// </summary>
			public Boolean IsReadOnly
			{
				get { return mIsReadOnly; }
				set { mIsReadOnly = value; }
			}

			/// <summary>
			/// Статус фиксированной коллекции
			/// </summary>
			public Boolean IsFixedSize
			{
				get 
				{ 
					return (mIsFixedSize);
				}
				set
				{
					mIsFixedSize = value;
				}
			}

			/// <summary>
			/// Статус синхронизации коллекции
			/// </summary>
			public Boolean IsSynchronized
			{
				get { return mArrayOfItems.IsSynchronized; }
			}

			/// <summary>
			/// Объект синхронизации
			/// </summary>
			public System.Object SyncRoot
			{
				get { return mArrayOfItems.SyncRoot; }
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Индексация списка
			/// </summary>
			/// <param name="index">Индекс элемента</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			System.Object IList.this[Int32 index]
			{
				get
				{
					return (mArrayOfItems[index]);
				}
				set
				{
					try
					{
						mArrayOfItems[index] = (TItem)value;

						if (mIsNotify)
						{

						}
					}
					catch (Exception exc)
					{
#if UNITY_2017_1_OR_NEWER
						UnityEngine.Debug.LogException(exc);
#else
						XLogger.LogException(exc);
#endif
					}

				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные списка предустановленными данными
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public ListArray()
				: this(INIT_MAX_COUNT)
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные списка указанными данными
			/// </summary>
			/// <param name="capacity">Начальная максимальная емкость списка</param>
			//---------------------------------------------------------------------------------------------------------
			public ListArray(Int32 capacity)
			{
				mMaxCount = capacity > INIT_MAX_COUNT ? capacity : INIT_MAX_COUNT;
				mCount = 0;
				mArrayOfItems = new TItem[mMaxCount];
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные списка указанными данными
			/// </summary>
			/// <param name="items">Список элементов</param>
			//---------------------------------------------------------------------------------------------------------
			public ListArray(IList<TItem> items)
			{
				if (items != null && items.Count > 0)
				{
					mMaxCount = items.Count;
					mCount = items.Count;
					mArrayOfItems = new TItem[mMaxCount];

					for (Int32 i = 0; i < items.Count; i++)
					{
						mArrayOfItems[i] = items[i];
					}
				}
				else
				{
					mMaxCount = INIT_MAX_COUNT;
					mCount = 0;
					mArrayOfItems = new TItem[mMaxCount];
				}
			}
			#endregion

			#region ======================================= ИНДЕКСАТОР ================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Индексация списка
			/// </summary>
			/// <param name="index">Индекс элемента</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public TItem this[Int32 index]
			{
				get { return mArrayOfItems[index]; }
				set
				{
					if (mIsNotify)
					{
						TItem original_item = mArrayOfItems[index];
						mArrayOfItems[index] = value;

						NotifyPropertyChanged(PropertyArgsIndexer);
						NotifyCollectionChanged(NotifyCollectionChangedAction.Replace, original_item, value, index);
					}
					else
					{
						mArrayOfItems[index] = value;
					}
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ IEnumerable ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение перечислителя
			/// </summary>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public IEnumerator<TItem> GetEnumerator()
			{
				return (new ListArrayEnumerator(this));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение перечислителя
			/// </summary>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			IEnumerator IEnumerable.GetEnumerator()
			{
				return (new ListArrayEnumerator(this));
			}
			#endregion

			#region ======================================= МЕТОДЫ IList ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Количество элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 Add(System.Object item)
			{
				try
				{
					Add((TItem)item);
					return mCount;
				}
				catch (Exception exc)
				{
#if (UNITY_2017_1_OR_NEWER)
					UnityEngine.Debug.LogException(exc);
#else
					XLogger.LogException(exc);
#endif
				}

				return mCount;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на наличие элемента в списке
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Статус наличия элемента в списке</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Contains(System.Object item)
			{
				return (Array.IndexOf(mArrayOfItems, item, 0, mCount) > -1);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение индекса элемента в списке
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Индекс элемента в списке</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 IndexOf(System.Object item)
			{
				return (Array.IndexOf(mArrayOfItems, item, 0, mCount));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вставка элемента в указанную позицию
			/// </summary>
			/// <param name="index">Позиция вставки</param>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public void Insert(Int32 index, System.Object item)
			{
				if(item is TItem item_type)
				{
					Insert(index, item_type);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public void Remove(System.Object item)
			{
				if (item is TItem item_type)
				{
					Remove(item_type);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Копирование элементов в указанный массив
			/// </summary>
			/// <param name="array">Целевой массив</param>
			/// <param name="index">Индекс с которого начинается копирование</param>
			//---------------------------------------------------------------------------------------------------------
			public void CopyTo(Array array, Int32 index)
			{
				mArrayOfItems.CopyTo(array, index);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка элемента списка по индексу с автоматическим увеличением размера при необходимости
			/// </summary>
			/// <param name="index">Индекс элемента списка</param>
			/// <param name="element">Элемент списка</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetAt(Int32 index, TItem element)
			{
				if (index >= mCount)
				{
					Add(element);
				}
				else
				{
					mArrayOfItems[index] = element;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение элемента списка по индексу
			/// </summary>
			/// <remarks>
			/// В случае если индекс выходит за границы списка, то возвращается последний элемент
			/// </remarks>
			/// <param name="index">Индекс элемента списка</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public TItem GetAt(Int32 index)
			{
				if (index >= mCount)
				{
					if (mCount == 0)
					{
						// Создаем объект по умолчанию
						Add(CreateItemDefault());
						return mArrayOfItems[0];
					}
					else
					{
						return mArrayOfItems[LastIndex];
					}
				}
				else
				{
					return mArrayOfItems[index];
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Резервирование места на определённое количество элементов с учетом существующих
			/// </summary>
			/// <param name="count">Количество элементов</param>
			//---------------------------------------------------------------------------------------------------------
			public void Reserve(Int32 count)
			{
				if (count <= 0) return;
				var new_count = mCount + count;

				if (new_count > mMaxCount)
				{
					while (mMaxCount < new_count)
					{
						mMaxCount <<= 1;
					}

					TItem[] items = new TItem[mMaxCount];
					Array.Copy(mArrayOfItems, items, mCount);
					mArrayOfItems = items;

					// Проходим по всем объектам и если надо создаем объект
					if (IsNullable)
					{
						for (Int32 i = mCount; i < new_count; i++)
						{
							if (mArrayOfItems[i] == null)
							{
								mArrayOfItems[i] = CreateItemDefault();
							}
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменение максимального количества элементов которая может вместить коллекция
			/// </summary>
			/// <param name="new_max_count">Новое максимальное количество элементов</param>
			//---------------------------------------------------------------------------------------------------------
			public void Resize(Int32 new_max_count)
			{
				// Если мы увеличиваем емкость массива
				if (new_max_count > mMaxCount)
				{
					mMaxCount = new_max_count;
					TItem[] items = new TItem[mMaxCount];
					Array.Copy(mArrayOfItems, items, mCount);
					mArrayOfItems = items;
				}
				else
				{
					// Максимальное количество элементов меньше текущего
					// Все ссылочные элементы в данном случае нам надо удалить
					if (new_max_count < mCount)
					{
						if (IsNullable)
						{
							for (Int32 i = new_max_count; i < mCount; i++)
							{
								mArrayOfItems[i] = default(TItem);
							}
						}

						mCount = new_max_count;
						mMaxCount = new_max_count;
						TItem[] items = new TItem[mMaxCount];
						Array.Copy(mArrayOfItems, items, mCount);
						mArrayOfItems = items;
					}
					else
					{
						// Простое уменьшение размера массива
						mMaxCount = new_max_count;
						TItem[] items = new TItem[mMaxCount];
						Array.Copy(mArrayOfItems, items, mCount);
						mArrayOfItems = items;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Определение емкости, равную фактическому числу элементов в списке, если это число меньше порогового значения
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void TrimExcess()
			{
				TItem[] items = new TItem[mCount];
				Array.Copy(mArrayOfItems, items, mCount);
				mArrayOfItems = items;
				mMaxCount = mCount;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Копирование элементов в указанный массив
			/// </summary>
			/// <param name="array_dest">Целевой массив</param>
			/// <param name="array_index">Позиция начала копирования</param>
			//---------------------------------------------------------------------------------------------------------
			public void CopyTo(TItem[] array_dest, Int32 array_index)
			{
				Array.Copy(mArrayOfItems, 0, array_dest, array_index, mCount);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка данных на прямую
			/// </summary>
			/// <param name="data">Данные</param>
			/// <param name="count">Количество данных</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetData(TItem[] data, Int32 count)
			{
				mArrayOfItems = data;
				mCount = count >= 0 ? count : 0;
				mMaxCount = mArrayOfItems.Length;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение непосредственно данных
			/// </summary>
			/// <returns>Данные</returns>
			//---------------------------------------------------------------------------------------------------------
			public TItem[] GetData()
			{
				return (mArrayOfItems);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка индекса для элементов
			/// </summary>
			/// <remarks>
			/// Индексы присваиваются согласно порядковому номеру элемента
			/// </remarks>
			//---------------------------------------------------------------------------------------------------------
			public void SetIndexElement()
			{
				if (IsIndexable)
				{
					for (Int32 i = 0; i < mCount; i++)
					{
						((ILotusIndexable)mArrayOfItems[i]).Index = i;
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации о переустановке коллекции
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void NotifyCollectionReset()
			{
				if (PropertyChanged != null) PropertyChanged(this, PropertyArgsCount);
				if (PropertyChanged != null) PropertyChanged(this, PropertyArgsIndexer);
				if(CollectionChanged != null)
				{
					CollectionChanged(this, CollectionArgsReset);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации об очистки коллекции
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void NotifyCollectionClear()
			{
				if (PropertyChanged != null) PropertyChanged(this, PropertyArgsCount);
				if (PropertyChanged != null) PropertyChanged(this, PropertyArgsIndexer);
				if (CollectionChanged != null)
				{
					CollectionChanged(this, CollectionArgsReset);
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ СОЗДАНИЯ ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание элемента с помощью конструктора по умолчанию
			/// </summary>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual TItem CreateItemDefault()
			{
				if (typeof(TItem).IsInterface)
				{
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogErrorFormat("Unable to create object by interface: {0}", typeof(TItem).Name);
#else
					XLogger.LogError(String.Format("Unable to create object by interface: {0}", typeof(TItem).Name));
#endif
					return (default(TItem));
				}

				if (typeof(TItem).IsAbstract)
				{
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogErrorFormat("Unable to create an abstract class object: {0}", typeof(TItem).Name);
#else
					XLogger.LogError(String.Format("Unable to create an abstract class object: {0}", typeof(TItem).Name));
#endif
					return (default(TItem));
				}

				return (XReflection.CreateInstance<TItem>());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание элемента с помощью конструктора по умолчанию
			/// </summary>
			/// <remarks>
			/// Происходит только создание элемента.
			/// Сам элемент НЕ добавляется в список элементов
			/// </remarks>
			/// <param name="element_name">Имя элемента</param>
			/// <param name="element_id">Идентификатор элемента</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual TItem CreateItemDefault(String element_name, Int32 element_id)
			{
				TItem item = CreateItemDefault();
				if(item is ILotusNameable nameable)
				{
					nameable.Name = element_name;
				}
				if (item is ILotusIdentifierId identifier_id)
				{
					identifier_id.Id = element_id;
				}
				return (item);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание элемента с помощью конструктора по умолчанию указанного типа
			/// </summary>
			/// <remarks>
			/// Происходит только создание элемента.
			/// Сам элемент НЕ добавляется в список элементов
			/// </remarks>
			/// <typeparam name="TItemEx">Тип модели</typeparam>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual TItemEx CreateItemOfType<TItemEx>() where TItemEx : TItem
			{
				if (typeof(TItemEx).IsInterface)
				{
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogErrorFormat("Unable to create object by interface: {0}", typeof(TItemEx).Name);
#else
					XLogger.LogError(String.Format("Unable to create object by interface: {0}", typeof(TItemEx).Name));
#endif
					return (default(TItemEx));
				}

				if (typeof(TItemEx).IsAbstract)
				{
#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogErrorFormat("Unable to create an abstract class object: {0}", typeof(TItemEx).Name);
#else
					XLogger.LogError(String.Format("Unable to create an abstract class object: {0}", typeof(TItemEx).Name));
#endif
					return (default(TItemEx));
				}

				return (XReflection.CreateInstance<TItemEx>());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание элемента с помощью конструктора по умолчанию указанного типа
			/// </summary>
			/// <remarks>
			/// Происходит только создание элемента.
			/// Сам элемент НЕ добавляется в список элементов
			/// </remarks>
			/// <typeparam name="TItemEx"></typeparam>
			/// <param name="element_name">Имя элемента</param>
			/// <param name="element_id">Идентификатор элемента</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual TItemEx CreateItemOfType<TItemEx>(String element_name, Int32 element_id) where TItemEx : TItem
			{
				TItemEx item = CreateItemOfType<TItemEx>();
				if (item is ILotusNameable nameable)
				{
					nameable.Name = element_name;
				}
				if (item is ILotusIdentifierId identifier_id)
				{
					identifier_id.Id = element_id;
				}
				return (item);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание элемента с помощью конструктора по умолчанию по указанному имени типа
			/// </summary>
			/// <remarks>
			/// Происходит только создание элемента.
			/// Сам элемент НЕ добавляется в список элементов
			/// </remarks>
			/// <param name="type_name">Имя типа элемента</param>
			/// <returns>Элемент</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual TItem CreateItemOfTypeName(String type_name)
			{
				return (default);
			}
			#endregion

			#region ======================================= МЕТОДЫ ДОБАВЛЕНИЯ =========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public void Add(TItem item)
			{
				// Если текущие количество элементов равно максимально возможному
				if (mCount == mMaxCount)
				{
					mMaxCount = mMaxCount * 2;
					TItem[] items = new TItem[mMaxCount];
					Array.Copy(mArrayOfItems, items, mCount);
					mArrayOfItems = items;
				}

				mArrayOfItems[mCount] = item;
				mCount++;

				if (mIsNotify)
				{
					if (PropertyChanged != null) PropertyChanged(this, PropertyArgsCount);
					if (PropertyChanged != null) PropertyChanged(this, PropertyArgsIndexer);
					NotifyCollectionChanged(NotifyCollectionChangedAction.Add, item, mCount - 1);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Дублирование последнего элемента и его добавление
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void AddDuplicateLastItem()
			{
				if (mCount > 0)
				{
					// Если текущие количество элементов равно максимально возможному
					if (mCount == mMaxCount)
					{
						mMaxCount = mMaxCount * 2;
						TItem[] items = new TItem[mMaxCount];
						Array.Copy(mArrayOfItems, items, mCount);
						mArrayOfItems = items;
					}

					TItem item = default(TItem);
					if (IsDuplicatable)
					{
						item = (ItemLast as ILotusDuplicate<TItem>).Duplicate();
					}
					else
					{
						if (ItemLast is ICloneable)
						{
							item = (TItem)(ItemLast as ICloneable).Clone();
						}
						else
						{
							if (IsNullable)
							{

							}
							else
							{
								item = ItemLast;
							}
						}
					}

					mArrayOfItems[mCount] = item;
					mCount++;

					if (mIsNotify)
					{
						if (PropertyChanged != null) PropertyChanged(this, PropertyArgsCount);
						if (PropertyChanged != null) PropertyChanged(this, PropertyArgsIndexer);
						NotifyCollectionChanged(NotifyCollectionChangedAction.Add, item, mCount - 1);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элементов
			/// </summary>
			/// <param name="items">Элементы</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddItems(params TItem[] items)
			{
				Reserve(items.Length);
				Array.Copy(items, 0, mArrayOfItems, mCount, items.Length);
				mCount += items.Length;

				if (mIsNotify)
				{
					if (PropertyChanged != null) PropertyChanged(this, PropertyArgsCount);
					if (PropertyChanged != null) PropertyChanged(this, PropertyArgsIndexer);
					Int32 original_count = mCount - items.Length;
					for (Int32 i = 0; i < items.Length; i++)
					{
						NotifyCollectionChanged(NotifyCollectionChangedAction.Add, items[i], original_count + i);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление элементов
			/// </summary>
			/// <param name="items">Элементы</param>
			//---------------------------------------------------------------------------------------------------------
			public void AddItems(IList<TItem> items)
			{
				Reserve(items.Count);
				for (Int32 i = 0; i < items.Count; i++)
				{
					mArrayOfItems[i + mCount] = items[i];
				}
				mCount += items.Count;

				if (mIsNotify)
				{
					if (PropertyChanged != null) PropertyChanged(this, PropertyArgsCount);
					if (PropertyChanged != null) PropertyChanged(this, PropertyArgsIndexer);
					Int32 original_count = mCount - items.Count;
					for (Int32 i = 0; i < items.Count; i++)
					{
						NotifyCollectionChanged(NotifyCollectionChangedAction.Add, items[i], original_count + i);
					}
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ВСТАВКИ ============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вставка элемента в указанную позицию
			/// </summary>
			/// <param name="index">Позиция вставки</param>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public void Insert(Int32 index, TItem item)
			{
				if (index >= mCount)
				{
					Add(item);
					return;
				}

				// Если текущие количество элементов равно максимально возможному
				if (mCount == mMaxCount)
				{
					mMaxCount = mMaxCount * 2;
					TItem[] items = new TItem[mMaxCount];
					Array.Copy(mArrayOfItems, items, mCount);
					mArrayOfItems = items;
				}

				Array.Copy(mArrayOfItems, index, mArrayOfItems, index + 1, mCount - index);
				mArrayOfItems[index] = item;
				mCount++;

				if (mIsNotify)
				{
					if (PropertyChanged != null) PropertyChanged(this, PropertyArgsCount);
					if (PropertyChanged != null) PropertyChanged(this, PropertyArgsIndexer);
					NotifyCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вставка элемента после указанного элемента
			/// </summary>
			/// <param name="original">Элемент после которого будет произведена вставка</param>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public void InsertAfter(TItem original, TItem item)
			{
				Int32 index = Array.IndexOf(mArrayOfItems, original);
				Insert(index + 1, item);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вставка элемента перед указанным элементом
			/// </summary>
			/// <param name="original">Элемент перед которым будет произведена вставка</param>
			/// <param name="item">Элемент</param>
			//---------------------------------------------------------------------------------------------------------
			public void InsertBefore(TItem original, TItem item)
			{
				Int32 index = Array.IndexOf(mArrayOfItems, original);
				Insert(index, item);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вставка элементов в указанную позицию
			/// </summary>
			/// <param name="index">Позиция вставки</param>
			/// <param name="items">Элементы</param>
			//---------------------------------------------------------------------------------------------------------
			public void InsertItems(Int32 index, params TItem[] items)
			{
				if (index >= mCount)
				{
					AddItems(items);
					return;
				}

				Reserve(items.Length);
				Array.Copy(mArrayOfItems, index, mArrayOfItems, index + items.Length, mCount - index);
				Array.Copy(items, 0, mArrayOfItems, index, items.Length);
				mCount += items.Length;

				if (mIsNotify)
				{
					if (PropertyChanged != null) PropertyChanged(this, PropertyArgsCount);
					if (PropertyChanged != null) PropertyChanged(this, PropertyArgsIndexer);
					Int32 original_count = mCount - items.Length;
					for (Int32 i = 0; i < items.Length; i++)
					{
						NotifyCollectionChanged(NotifyCollectionChangedAction.Add, items[i], index + i);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вставка элементов в указанную позицию
			/// </summary>
			/// <param name="index">Позиция вставки</param>
			/// <param name="items">Элементы</param>
			//---------------------------------------------------------------------------------------------------------
			public void InsertItems(Int32 index, IList<TItem> items)
			{
				if (index >= mCount)
				{
					AddItems(items);
					return;
				}

				Reserve(items.Count);
				Array.Copy(mArrayOfItems, index, mArrayOfItems, index + items.Count, mCount - index);
				for (Int32 i = 0; i < items.Count; i++)
				{
					mArrayOfItems[i + index] = items[i];
				}
				mCount += items.Count;

				if (mIsNotify)
				{
					if (PropertyChanged != null) PropertyChanged(this, PropertyArgsCount);
					if (PropertyChanged != null) PropertyChanged(this, PropertyArgsIndexer);
					Int32 original_count = mCount - items.Count;
					for (Int32 i = 0; i < items.Count; i++)
					{
						NotifyCollectionChanged(NotifyCollectionChangedAction.Add, items[i], index + i);
					}
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ УДАЛЕНИЯ ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Статус успешности удаления</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Remove(TItem item)
			{
				Int32 index = Array.IndexOf(mArrayOfItems, item, 0, mCount);
				if (index != -1)
				{
					mCount--;
					Array.Copy(mArrayOfItems, index + 1, mArrayOfItems, index, mCount - index);
					mArrayOfItems[mCount] = default(TItem);

					if (mIsNotify)
					{
						if (PropertyChanged != null) PropertyChanged(this, PropertyArgsCount);
						if (PropertyChanged != null) PropertyChanged(this, PropertyArgsIndexer);
						NotifyCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
					}


					return true;
				}

				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элементов
			/// </summary>
			/// <param name="items">Элементы</param>
			/// <returns>Количество удаленных элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 RemoveItems(params TItem[] items)
			{
				Int32 count = 0;
				for (Int32 i = 0; i < items.Length; i++)
				{
					Int32 index = Array.IndexOf(mArrayOfItems, items[i], 0, mCount);
					if (index != -1)
					{
						mCount--;
						Array.Copy(mArrayOfItems, index + 1, mArrayOfItems, index, mCount - index);
						mArrayOfItems[mCount] = default(TItem);
						count++;

						if (mIsNotify)
						{
							if (PropertyChanged != null) PropertyChanged(this, PropertyArgsIndexer);
							NotifyCollectionChanged(NotifyCollectionChangedAction.Remove, items[i], index);
						}
					}
				}

				return count;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элементов
			/// </summary>
			/// <param name="items">Элементы</param>
			/// <returns>Количество удаленных элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 RemoveItems(IList<TItem> items)
			{
				Int32 count = 0;
				for (Int32 i = 0; i < items.Count; i++)
				{
					Int32 index = Array.IndexOf(mArrayOfItems, items[i], 0, mCount);
					if (index != -1)
					{
						mCount--;
						Array.Copy(mArrayOfItems, index + 1, mArrayOfItems, index, mCount - index);
						mArrayOfItems[mCount] = default(TItem);
						count++;

						if (mIsNotify)
						{
							if (PropertyChanged != null) PropertyChanged(this, PropertyArgsCount);
							if (PropertyChanged != null) PropertyChanged(this, PropertyArgsIndexer);
							NotifyCollectionChanged(NotifyCollectionChangedAction.Remove, items[i], index);
						}
					}
				}

				return count;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элементов с указанного индекса и до конца коллекции
			/// </summary>
			/// <param name="index">Индекс начала удаления элементов</param>
			//---------------------------------------------------------------------------------------------------------
			public void RemoveItemsEnd(Int32 index)
			{
				Int32 count = mCount - index;
				RemoveRange(index, count);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элементов с определенного диапазона
			/// </summary>
			/// <param name="index">Индекс начала удаления элементов</param>
			/// <param name="count">Количество удаляемых элементов</param>
			//---------------------------------------------------------------------------------------------------------
			public void RemoveRange(Int32 index, Int32 count)
			{
#if UNITY_EDITOR
				if (index < 0)
				{
					UnityEngine.Debug.LogErrorFormat("Index is less than zero: <{0}> (Return)", index);
					index = 0;
				}
				if (count < 0)
				{
					UnityEngine.Debug.LogErrorFormat("Count is less than zero: <{0}> (Return)", count);
					count = 1;
				}
				if (mCount - index < count)
				{
					UnityEngine.Debug.LogErrorFormat("The index <{0}> + count <{1}> is greater than the number of elements: <2> (Return)", index, count, mCount);
					return;
				}
#endif
				if (count > 0)
				{
					Int32 i = mCount;
					mCount -= count;

					if (index < mCount)
					{
						Array.Copy(mArrayOfItems, index + count, mArrayOfItems, index, mCount - index);
					}

					Array.Clear(mArrayOfItems, mCount, count);

					if (mIsNotify)
					{
						if (PropertyChanged != null) PropertyChanged(this, PropertyArgsCount);
						if (PropertyChanged != null) PropertyChanged(this, PropertyArgsIndexer);
						NotifyCollectionReset();
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление элемента
			/// </summary>
			/// <param name="index">Индекс удаляемого элемента</param>
			//---------------------------------------------------------------------------------------------------------
			public void RemoveAt(Int32 index)
			{
#if UNITY_EDITOR
				if (index < 0)
				{
					UnityEngine.Debug.LogErrorFormat("Index is less than zero: <{0}> (Return)", index);
					return;
				}
				if (index > LastIndex)
				{
					UnityEngine.Debug.LogErrorFormat("The index is greater than the number of elements: <{0}> (Return)", index);
					return;
				}
#endif

				if (mIsNotify)
				{
					TItem temp = mArrayOfItems[index];

					mCount--;
					Array.Copy(mArrayOfItems, index + 1, mArrayOfItems, index, mCount - index);
					mArrayOfItems[mCount] = default(TItem);

					if (PropertyChanged != null) PropertyChanged(this, PropertyArgsCount);
					if (PropertyChanged != null) PropertyChanged(this, PropertyArgsIndexer);
					NotifyCollectionChanged(NotifyCollectionChangedAction.Remove, temp, index);
				}
				else
				{
					mCount--;
					Array.Copy(mArrayOfItems, index + 1, mArrayOfItems, index, mCount - index);
					mArrayOfItems[mCount] = default(TItem);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление первого элемента
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void RemoveFirst()
			{
				RemoveAt(0);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление последнего элемента
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void RemoveLast()
			{
				if (mCount > 0)
				{
					RemoveAt(mCount - 1);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление дубликатов элементов
			/// </summary>
			/// <returns>Количество дубликатов элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 RemoveDuplicates()
			{
				TItem[] unique = new TItem[mCount];
				Int32 count = 0;
				for (Int32 i = 0; i < mCount; i++)
				{
					TItem item = mArrayOfItems[i];
					Int32 index = Array.IndexOf(unique, item);
					if(index == -1)
					{
						unique[count] = item;
						count++;
					}
				}

				// У нас есть дубликаты
				if (count < mCount)
				{
					Array.Copy(unique, 0, mArrayOfItems, 0, count);

					for (Int32 i = count; i < mCount; i++)
					{
						mArrayOfItems[i] = default(TItem);
					}

					Int32 delta = mCount - count;
					mCount = count;

					if (mIsNotify)
					{
						NotifyCollectionReset();
					}

					return delta;
				}

				return 0;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление всех элементов, удовлетворяющих условиям указанного предиката
			/// </summary>
			/// <param name="match">Предикат</param>
			/// <returns>Количество удаленных элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 RemoveAll(Predicate<TItem> match)
			{
				Int32 free_index = 0;   // the first free slot in items array

				// Find the first item which needs to be removed.
				while (free_index < mCount && !match(mArrayOfItems[free_index])) free_index++;
				if (free_index >= mCount) return 0;

				Int32 current = free_index + 1;
				while (current < mCount)
				{
					// Find the first item which needs to be kept.
					while (current < mCount && match(mArrayOfItems[current])) current++;

					if (current < mCount)
					{
						// copy item to the free slot.
						mArrayOfItems[free_index++] = mArrayOfItems[current++];
					}
				}

				Array.Clear(mArrayOfItems, free_index, mCount - free_index);
				Int32 result = mCount - free_index;
				mCount = free_index;

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обрезать список сначала до указанного элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <param name="included">Включать указанный элемент в удаление</param>
			/// <returns>Количество удаленных элементов, -1 если элемент не найден</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 TrimStart(TItem item, Boolean included = true)
			{
				Int32 index = Array.IndexOf(mArrayOfItems, item);
				if(index > -1)
				{
					if(index == 0)
					{
						if (included)
						{
							// Удаляем первый элемент
							RemoveFirst();
							return (1);
						}
						else
						{
							return (0);
						}
					}
					else
					{
						if (index == LastIndex)
						{
							// Удаляем все элементы
							if (included)
							{
								Int32 count = mCount;
								Clear();
								return (count);
							}
							else
							{
								// Удаляем элементы до последнего
								Int32 count = mCount - 1;
								RemoveRange(0, count);
								return (count);
							}
						}
						else
						{
							if (included)
							{
								RemoveRange(0, index + 1);
								return (index + 1);
							}
							else
							{
								RemoveRange(0, index);
								return (index);
							}
						}
					}
				}

				return (-1);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обрезать список с конца до указанного элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <param name="included">Включать указанный элемент в удаление</param>
			/// <returns>Количество удаленных элементов, -1 если элемент не найден</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 TrimEnd(TItem item, Boolean included = true)
			{
				Int32 index = Array.LastIndexOf(mArrayOfItems, item);
				if (index > -1)
				{
					if (index == 0)
					{
						// Удаляем все элементы
						if (included)
						{
							Int32 count = mCount;
							Clear();
							return (count);
						}
						else
						{
							// Удаляем элементы до первого
							Int32 count = mCount - 1;
							RemoveRange(1, count);
							return (count);
						}
					}
					else
					{
						if (index == LastIndex)
						{
							// Удаляем последний элемент
							if (included)
							{
								RemoveLast();
								return (1);
							}
							else
							{
								return (0);
							}
						}
						else
						{
							if (included)
							{
								Int32 count = mCount - index;
								RemoveRange(index, count);
								return (count);
							}
							else
							{
								Int32 count = mCount - index - 1;
								RemoveRange(index + 1, count);
								return (count);
							}
						}
					}
				}

				return (-1);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка списка
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Clear()
			{
				Array.Clear(mArrayOfItems, 0, mCount);
				mCount = 0;

				if (mIsNotify)
				{
					NotifyCollectionClear();
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ МНОЖЕСТВ ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Присвоение новых элементов списку
			/// </summary>
			/// <param name="items">Элементы</param>
			//---------------------------------------------------------------------------------------------------------
			public void AssignItems(params TItem[] items)
			{
				Reserve(items.Length - mCount);
				Array.Copy(items, 0, mArrayOfItems, 0, items.Length);
				mCount = items.Length;

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Присвоение новых элементов списку
			/// </summary>
			/// <param name="items">Элементы</param>
			//---------------------------------------------------------------------------------------------------------
			public void AssignItems(IList<TItem> items)
			{
				Reserve(items.Count - mCount);
				for (Int32 i = 0; i < items.Count; i++)
				{
					mArrayOfItems[i] = items[i];
				}
				mCount = items.Count;

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Объединение элементов и существующих элементов списка
			/// </summary>
			/// <remarks>
			/// Под объедением понимается присвоение только тех переданных элементов которых нет в исходном списке
			/// </remarks>
			/// <param name="items">Элементы</param>
			//---------------------------------------------------------------------------------------------------------
			public void UnionItems(params TItem[] items)
			{
				Reserve(items.Length);
				for (Int32 i = 0; i < items.Length; i++)
				{
					if (Array.IndexOf(mArrayOfItems, items[i]) == -1)
					{
						Add(items[i]);
					}
				}
				mCount = items.Length;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Объединение элементов и существующих элементов списка
			/// </summary>
			/// <remarks>
			/// Под объедением понимается присвоение только тех переданных элементов которых нет в исходном списке
			/// </remarks>
			/// <param name="items">Элементы</param>
			//---------------------------------------------------------------------------------------------------------
			public void UnionItems(IList<TItem> items)
			{
				Reserve(items.Count);
				for (Int32 i = 0; i < items.Count; i++)
				{
					if(Array.IndexOf(mArrayOfItems, items[i]) == -1)
					{
						Add(items[i]);
					}
				}
				mCount = items.Count;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Разница между элементами списка и элементами указанного списка
			/// </summary>
			/// <remarks>
			/// Под разницей понимается список элементов, которые есть в исходном списке и нет в переданном. 
			/// По-другому это те элементы из исходного списка которые необходимо удалить что бы он соответствовал 
			/// переданному списку
			/// </remarks>
			/// <param name="items">Элементы</param>
			/// <returns>Список элементов списка</returns>
			//---------------------------------------------------------------------------------------------------------
			public ListArray<TItem> DifferenceItems(IList<TItem> items)
			{
				ListArray<TItem> difference = new ListArray<TItem>();

				for (Int32 i = 0; i < mCount; i++)
				{
					if (items.Contains(mArrayOfItems[i]) == false)
					{
						difference.Add(mArrayOfItems[i]);
					}
				}

				return (difference);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Разница между элементами списка и элементами указанного списка
			/// </summary>
			/// <remarks>
			/// Под разницей понимается список элементов, которые есть в исходном списке и нет в переданном. 
			/// По-другому это те элементы из исходного списка которые необходимо удалить что бы он соответствовал 
			/// переданному списку
			/// </remarks>
			/// <example>
			/// Исходный список
			/// [0, 2, 4, 7, 5]
			/// Переданный список
			/// [0, 4, 7, 12, 15, 7]
			/// Результат
			/// [2, 5]
			/// </example>
			/// <param name="items">Элементы</param>
			/// <returns>Список элементов списка</returns>
			//---------------------------------------------------------------------------------------------------------
			public ListArray<TItem> DifferenceItems(params TItem[] items)
			{
				ListArray<TItem> difference = new ListArray<TItem>();

				for (Int32 i = 0; i < mCount; i++)
				{
					if (items.Contains(mArrayOfItems[i]) == false)
					{
						difference.Add(mArrayOfItems[i]);
					}
				}

				return (difference);
			}
			#endregion

			#region ======================================= МЕТОДЫ ПОИСКА =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на наличие элемента в списке
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Статус наличия</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Contains(TItem item)
			{
				return Array.IndexOf(mArrayOfItems, item, 0, mCount) != -1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск с начала списка индекса указанного элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Индекс элемента</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 IndexOf(TItem item)
			{
				return Array.IndexOf(mArrayOfItems, item, 0, mCount);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск с конца списка индекса указанного элемента
			/// </summary>
			/// <param name="item">Элемент</param>
			/// <returns>Индекс элемента</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 LastIndexOf(TItem item)
			{
				return Array.LastIndexOf(mArrayOfItems, item, 0, mCount);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск с начала списка индекса элемента удовлетворяющему указанному предикату
			/// </summary>
			/// <param name="match">Предикат</param>
			/// <returns>Индекс элемента</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 Find(Predicate<TItem> match)
			{
				for (Int32 i = 0; i < mCount; i++)
				{
					if (match(mArrayOfItems[i])) { return i; }
				}

				return -1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск с конца списка индекса элемента удовлетворяющему указанному предикату
			/// </summary>
			/// <param name="match">Предикат</param>
			/// <returns>Индекс элемента</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 FindLast(Predicate<TItem> match)
			{
				for (Int32 i = LastIndex; i >= 0; i--)
				{
					if (match(mArrayOfItems[i])) { return i; }
				}

				return -1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск с начала списка элемента удовлетворяющему указанному предикату
			/// </summary>
			/// <param name="match">Предикат</param>
			/// <returns>Элемент или значение по умолчанию</returns>
			//---------------------------------------------------------------------------------------------------------
			public TItem Search(Predicate<TItem> match)
			{
				for (Int32 i = 0; i < mCount; i++)
				{
					if (match(mArrayOfItems[i])) { return (mArrayOfItems[i]); }
				}

				return default;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск с конца списка элемента удовлетворяющему указанному предикату
			/// </summary>
			/// <param name="match">Предикат</param>
			/// <returns>Элемент или значение по умолчанию</returns>
			//---------------------------------------------------------------------------------------------------------
			public TItem SearchLast(Predicate<TItem> match)
			{
				for (Int32 i = LastIndex; i >= 0; i--)
				{
					if (match(mArrayOfItems[i])) { return (mArrayOfItems[i]); }
				}

				return default;
			}
			#endregion

			#region ======================================= МЕТОДЫ ПОЛЬЗОВАТЕЛЬСКИХ ОПЕРАЦИЙ ==========================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка элементов списка на удовлетворение указанного предиката
			/// </summary>
			/// <remarks>
			/// Список удовлетворяет условию предиката если каждый его элемент удовлетворяет условию предиката
			/// </remarks>
			/// <param name="match">Предикат проверки</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean CheckAll(Predicate<TItem> match)
			{
				Boolean result = true;
				for (Int32 i = 0; i < mCount; i++)
				{
					if(match(mArrayOfItems[i]) == false)
					{
						result = false;
						break;
					}
				}
				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка элементов списка на удовлетворение указанного предиката
			/// </summary>
			/// <remarks>
			/// Список удовлетворяет условию предиката если хотя бы один его элемент удовлетворяет условию предиката
			/// </remarks>
			/// <param name="match">Предикат проверки</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual Boolean CheckOne(Predicate<TItem> match)
			{
				Boolean result = false;
				for (Int32 i = 0; i < mCount; i++)
				{
					if (match(mArrayOfItems[i]))
					{
						result = true;
						break;
					}
				}
				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Посещение элементов списка указанным посетителем
			/// </summary>
			/// <param name="on_visitor">Делегат посетителя</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Visit(Action<TItem> on_visitor)
			{
				for (Int32 i = 0; i < mCount; i++)
				{
					on_visitor(mArrayOfItems[i]);
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ МОДИФИКАЦИИ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обмен местами элементов списка
			/// </summary>
			/// <param name="old_index">Старая позиция</param>
			/// <param name="new_index">Новая позиция</param>
			//---------------------------------------------------------------------------------------------------------
			public void Swap(Int32 old_index, Int32 new_index)
			{
				TItem temp = mArrayOfItems[old_index];
				mArrayOfItems[old_index] = mArrayOfItems[new_index];
				mArrayOfItems[new_index] = temp;

				if (mIsNotify)
				{
					if (PropertyChanged != null) PropertyChanged(this, PropertyArgsIndexer);
					NotifyCollectionChanged(NotifyCollectionChangedAction.Move, temp, new_index, old_index);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перемещение элемента в новую позицию
			/// </summary>
			/// <param name="old_index">Старая позиция</param>
			/// <param name="new_index">Новая позиция</param>
			//---------------------------------------------------------------------------------------------------------
			public void Move(Int32 old_index, Int32 new_index)
			{
				TItem temp = mArrayOfItems[old_index];
				RemoveAt(old_index);
				Insert(new_index, temp);

				if (mIsNotify)
				{
					if (PropertyChanged != null) PropertyChanged(this, PropertyArgsIndexer);
					NotifyCollectionChanged(NotifyCollectionChangedAction.Move, temp, new_index, old_index);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перемещение элемента списка вниз
			/// </summary>
			/// <remarks>
			/// При перемещении вниз индекс элемент увеличивается
			/// </remarks>
			/// <param name="element_index">Индекс перемещаемого элемента</param>
			//---------------------------------------------------------------------------------------------------------
			public void MoveDown(Int32 element_index)
			{
				Int32 next = (element_index + 1) % mCount;
				Swap(element_index, next);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перемещение элемента списка вверх
			/// </summary>
			/// <remarks>
			/// При перемещении вниз индекс элемент уменьшается
			/// </remarks>
			/// <param name="element_index">Индекс перемещаемого элемента</param>
			//---------------------------------------------------------------------------------------------------------
			public void MoveUp(Int32 element_index)
			{
				Int32 previous = element_index - 1;
				if (previous < 0) previous = LastIndex;
				Swap(element_index, previous);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Циклическое смещение элементов списка вниз
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void ShiftDown()
			{
				XExtensionCollections.Shift(this, true);

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Циклическое смещение элементов списка вверх
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void ShiftUp()
			{
				XExtensionCollections.Shift(this, false);

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Перетасовка элементов списка
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Shuffle()
			{
				XExtensionCollections.Shuffle(this);

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сортировка списка по возрастанию
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void SortAscending()
			{
				Array.Sort(mArrayOfItems, 0, mCount);

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сортировка списка по убыванию
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void SortDescending()
			{
				Array.Sort(mArrayOfItems, 0, mCount);
				Array.Reverse(mArrayOfItems, 0, mCount);

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сортировка элементов списка посредством делегата
			/// </summary>
			/// <param name="comparer">Делегат сравнивающий два объекта одного типа</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Sort(IComparer<TItem> comparer)
			{
				Array.Sort(mArrayOfItems, 0, mCount, comparer);

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сортировка элементов списка посредством делегата
			/// </summary>
			/// <remarks>
			/// Внимание, методом надо пользоваться с осторожностью так он учитывает реальный размер массива
			/// </remarks>
			/// <param name="comparison">Делегат сравнивающий два объекта одного типа</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void Sort(Comparison<TItem> comparison)
			{
				IComparer<TItem> comparer = new FunctorComparer(comparison);

				Array.Sort(mArrayOfItems, 0, mCount, comparer);

				if (mIsNotify)
				{
					NotifyCollectionReset();
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ БЛИЖАЙШЕГО ИНДЕКСА =================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск ближайшего индекса элемента по его значению, не больше указанного значения
			/// </summary>
			/// <remarks>
			/// Применяется только для списков, отсортированных по возрастанию
			/// </remarks>
			/// <example>
			/// [2, 4, 10, 15] -> ищем элемент со значением 1, вернет индекс 0
			/// [2, 4, 10, 15] -> ищем элемент со значением 2, вернет индекс 0
			/// [2, 4, 10, 15] -> ищем элемент со значением 3, вернет индекс 0
			/// [2, 4, 10, 15] -> ищем элемент со значением 4, вернет индекс 1
			/// [2, 4, 10, 15] -> ищем элемент со значением 9, вернет индекс 1
			/// [2, 4, 10, 15] -> ищем элемент со значением 20, вернет индекс 3
			/// </example>
			/// <param name="item">Элемент</param>
			/// <returns>Ближайший индекс элемента</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetClosestIndex(TItem item)
			{
				// Если элемент равен или меньше первого возвращаем нулевой индекс
				if (ComparerDefault.Compare(item, ItemFirst) <= 0)
				{
					return (0);
				}

				// Если элемент равен или больше последнего возвращаем последний индекс
				if (ComparerDefault.Compare(item, ItemLast) >= 0)
				{
					return (LastIndex);
				}

				for (Int32 i = 1; i < mCount; i++)
				{
					// Получаем статус сравнения
					Int32 status = ComparerDefault.Compare(item, mArrayOfItems[i]);

					// Если элемент равен возвращаем данный индекс
					if (status == 0)
					{
						return (i);
					}
					else
					{
						// Если он меньше предыдущего то возвращаем предыдущий индекс
						if (status == -1)
						{
							return (i - 1);
						}
					}
				}

				return (LastIndex);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обрезать список сначала до ближайшего указанного элемента
			/// </summary>
			/// <remarks>
			/// Применяется только для списков, отсортированных по возрастанию.
			/// Включаемый элемент будет удален если он совпадает
			/// </remarks>
			/// <param name="item">Элемент</param>
			/// <param name="included">Включать указанный элемент в удаление</param>
			/// <returns>Количество удаленных элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 TrimClosestStart(TItem item, Boolean included = true)
			{
				Int32 comprare_first = ComparerDefault.Compare(item, ItemFirst);
				Int32 comprare_last = ComparerDefault.Compare(item, ItemLast);

				// Элемент находиться за пределами списка - в начале
				if (comprare_first <= 0)
				{
					// Удаляем первый элемент если он включен и равен
					if (comprare_first == 0 && included)
					{
						RemoveFirst();
						return (1);
					}
					else
					{
						return (0);
					}
				}

				// Элемент находиться за пределами списка - в конце 
				if (comprare_last >= 0)
				{
					// Если он совпал последним элементом и при этом его не надо удалять
					if (comprare_last == 0 && included == false)
					{
						// Удаляем элементы до последнего
						Int32 count = Count - 1;
						RemoveRange(0, count);
						return (count);
					}
					else
					{
						// Удаляем все элементы
						Int32 count = mCount;
						Clear();
						return (count);
					}
				}

				// Элемент находиться в пределах списка
				Int32 max_count = Count - 1;
				for (Int32 i = 1; i < max_count; i++)
				{
					Int32 status = ComparerDefault.Compare(item, mArrayOfItems[i]);

					// Элемент полностью совпал
					if (status == 0)
					{
						if (included)
						{
							RemoveRange(0, i + 1);
							return (i + 1);
						}
						else
						{
							RemoveRange(0, i);
							return (i);
						}
					}
					else
					{
						// Только если он меньше
						if (status == -1)
						{
							RemoveRange(0, i);
							return (i);
						}
					}
				}

				return (0);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обрезать список с конца до ближайшего указанного элемента
			/// </summary>
			/// <remarks>
			/// Применяется только для списков, отсортированных по возрастанию.
			/// Включаемый элемент будет удален если он совпадает
			/// </remarks>
			/// <param name="item">Элемент</param>
			/// <param name="included">Включать указанный элемент в удаление</param>
			/// <returns>Количество удаленных элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 TrimClosestEnd(TItem item, Boolean included = true)
			{
				Int32 comprare_first = ComparerDefault.Compare(item, ItemFirst);
				Int32 comprare_last = ComparerDefault.Compare(item, ItemLast);

				// Элемент находиться за пределами списка - в начале
				if (comprare_first <= 0)
				{
					// Удаляем до первого элементы если он выключен и совпадает
					if (comprare_first == 0 && included == false)
					{
						Int32 count = Count - 1;
						RemoveRange(1, count);
						return (count);
					}
					else
					{
						// Удаляем все элементы
						Int32 count = Count;
						Clear();
						return (count);
					}
				}

				// Элемент находиться за пределами списка - в конце 
				if (comprare_last >= 0)
				{
					// Если он совпал последним элементом и при этом его надо удалять
					if (comprare_last == 0 && included)
					{
						// Удаляем последний элемент
						RemoveLast();
						return (1);
					}
					else
					{
						return (0);
					}
				}

				// Элемент находиться в пределах списка
				for (Int32 i = Count - 1; i >= 1; i--)
				{
					Int32 status = ComparerDefault.Compare(item, mArrayOfItems[i]);

					// Элемент полностью совпал
					if (status == 0)
					{
						if (included)
						{
							Int32 count = mCount;
							RemoveItemsEnd(i);
							return (count - i);
						}
						else
						{
							Int32 count = mCount;
							RemoveItemsEnd(i + 1);
							return (count - i - 1);
						}
					}
					else
					{
						// Только если он больше
						if (status == 1)
						{
							Int32 count = mCount;
							RemoveItemsEnd(i + 1);
							return (count - i - 1);
						}
					}
				}

				return (0);
			}
			#endregion

			#region ======================================= МЕТОДЫ ГРУППИРОВАНИЯ ======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка групп элементов сгруппированных по указанному свойству
			/// </summary>
			/// <param name="property_name">Имя свойства</param>
			/// <returns>Список групп элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual ListArray<ListArray<TItem>> GetGroupedByProperty(String property_name)
			{
				// Список групп
				ListArray<ListArray<TItem>> groups = new ListArray<ListArray<TItem>>();

				// Cписок уникальных значений
				ListArray<System.Object> unique_list = new ListArray<System.Object>();

				if (IsNullable)
				{
					// Будем получать свойство для каждого элемента так как возможно наследование
					for (Int32 i = 0; i < mCount; i++)
					{
						// Получем свойство
						PropertyInfo property_info = mArrayOfItems[i].GetType().GetProperty(property_name, BindingFlags.Public | BindingFlags.Instance);

						if (property_info != null)
						{
							// Ищем совпадение в существующем списке
							Int32 find_index = unique_list.Find((System.Object value) =>
							{
								System.Object property_value = property_info.GetValue(mArrayOfItems[i], null);
								return (value.Equals(property_value));
							});

							// Совпадение не нашли значит это уникальный элемент
							if (find_index == -1)
							{
								// Добавляем в уникальные значений
								unique_list.Add(property_info.GetValue(mArrayOfItems[i], null));

								// Создаем группу
								ListArray<TItem> group = new ListArray<TItem>();

								// Добавляем туда данный элемент
								group.Add(mArrayOfItems[i]);

								// Добавляем саму группу
								groups.Add(group);
							}
							else
							{
								// Это не уникальный элемент, а значит группа такая уже есть
								// Получаем группу
								ListArray<TItem> group = groups[find_index];

								// Добавляем туда данный элемент
								group.Add(mArrayOfItems[i]);
							}
						}
					}
				}
				else
				{
					// Получаем свойство 1 раз
					PropertyInfo property_info = typeof(TItem).GetType().GetProperty(property_name, BindingFlags.Public | BindingFlags.Instance);
					if (property_info != null)
					{
						for (Int32 i = 0; i < mCount; i++)
						{
							// Ищем совпадение в существующем списке
							Int32 find_index = unique_list.Find((System.Object value) =>
							{
								return (value.Equals(property_info.GetValue(mArrayOfItems[i], null)));
							});

							// Совпадение не нашли значит это уникальный элемент
							if (find_index == -1)
							{
								// Создаем группу
								ListArray<TItem> group = new ListArray<TItem>();

								// Добавляем туда данный элемент
								group.Add(mArrayOfItems[i]);

								// Добавляем саму группу
								groups.Add(group);
							}
							else
							{
								// Это не уникальный элемент, а значит группа такая уже есть
								// Получаем группу
								ListArray<TItem> group = groups[find_index];

								// Добавляем туда данный элемент
								group.Add(mArrayOfItems[i]);
							}

						}
					}
				}

				return (groups);
			}
			#endregion

			#region ======================================= МЕТОДЫ ПОЛУЧЕНИЯ КОЛЛЕКЦИЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение дубликата коллекции
			/// </summary>
			/// <returns>Дубликат коллекции</returns>
			//---------------------------------------------------------------------------------------------------------
			public ListArray<TItem> GetItemsDuplicate()
			{
				ListArray<TItem> items = new ListArray<TItem>(this.MaxCount);
				Array.Copy(mArrayOfItems, 0, items.mArrayOfItems, 0, mCount);
				items.mCount = Count;
				return (items);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение различающихся (уникальных) элементов списка
			/// </summary>
			/// <returns>Список различающихся(уникальных) элементов списка</returns>
			//---------------------------------------------------------------------------------------------------------
			public ListArray<TItem> GetItemsDistinct()
			{
				ListArray<TItem> unique_list = new ListArray<TItem>();

				for (Int32 i = 0; i < mCount; i++)
				{
					// Ищем совпадение в существующем списке
					Int32 find_index = unique_list.Find((TItem value) =>
					{
						return (value.Equals(mArrayOfItems[i]));
					});

					// Совпадение не нашли значит это уникальный элемент
					if (find_index == -1)
					{
						// Добавляем в уникальные значений
						unique_list.Add(mArrayOfItems[i]);
					}
				}

				return (unique_list);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка уникальных значений указанного свойства
			/// </summary>
			/// <remarks>
			/// Используется рефлексия
			/// </remarks>
			/// <param name="property_name">Имя свойства</param>
			/// <returns>Список уникальных свойств</returns>
			//---------------------------------------------------------------------------------------------------------
			public ListArray<System.Object> GetDistinctValueFromPropertyName(String property_name)
			{
				ListArray<System.Object> unique_list = new ListArray<System.Object>();

				if (IsNullable)
				{
					// Будем получать свойство и каждого элемента так как возможно наследование
					for (Int32 i = 0; i < mCount; i++)
					{
						// Получем свойство
						PropertyInfo property_info = mArrayOfItems[i].GetType().GetProperty(property_name, BindingFlags.Public | BindingFlags.Instance);

						if (property_info != null)
						{
							// Ищем совпадение в существующем списке
							Int32 find_index = unique_list.Find((System.Object value) =>
							{
								return (value.Equals(property_info.GetValue(mArrayOfItems[i], null)));
							});

							// Совпадение не нашли значит это уникальный элемент
							if (find_index == -1)
							{
								// Добавляем в уникальные значений
								unique_list.Add(property_info.GetValue(mArrayOfItems[i], null));
							}
						}
					}
				}
				else
				{
					// Получаем свойство 1 раз
					PropertyInfo property_info = typeof(TItem).GetType().GetProperty(property_name, BindingFlags.Public | BindingFlags.Instance);
					if (property_info != null)
					{
						for (Int32 i = 0; i < mCount; i++)
						{
							// Ищем совпадение в существующем списке
							Int32 find_index = unique_list.Find((System.Object value) =>
							{
								return (value.Equals(property_info.GetValue(mArrayOfItems[i], null)));
							});

							// Совпадение не нашли значит это уникальный элемент
							if (find_index == -1)
							{
								// Добавляем в уникальные значений
								unique_list.Add(property_info.GetValue(mArrayOfItems[i], null));
							}

						}
					}
				}

				return (unique_list);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка элементов по указанному условию
			/// </summary>
			/// <param name="comparison">Оператор сравнения</param>
			/// <param name="match">Объект с которым производится сравнение</param>
			/// <returns>Список элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public ListArray<TItem> GetItemsWhere(TComparisonOperator comparison, TItem match)
			{
				ListArray<TItem> result = new ListArray<TItem>(mMaxCount);

				switch (comparison)
				{
					case TComparisonOperator.Equality:
						{
							for (Int32 i = 0; i < mCount; i++)
							{
								if (ComparerDefault.Compare(mArrayOfItems[i], match) == 0)
								{
									result.Add(mArrayOfItems[i]);
								}
							}
						}
						break;
					case TComparisonOperator.Inequality:
						{
							for (Int32 i = 0; i < mCount; i++)
							{
								if (ComparerDefault.Compare(mArrayOfItems[i], match) != 0)
								{
									result.Add(mArrayOfItems[i]);
								}
							}
						}
						break;
					case TComparisonOperator.LessThan:
						{
							for (Int32 i = 0; i < mCount; i++)
							{
								if (ComparerDefault.Compare(mArrayOfItems[i], match) == -1)
								{
									result.Add(mArrayOfItems[i]);
								}
							}
						}
						break;
					case TComparisonOperator.LessThanOrEqual:
						{
							for (Int32 i = 0; i < mCount; i++)
							{
								Int32 status = ComparerDefault.Compare(mArrayOfItems[i], match);
								if (status == 0 || status == -1)
								{
									result.Add(mArrayOfItems[i]);
								}
							}
						}
						break;
					case TComparisonOperator.GreaterThan:
						{
							for (Int32 i = 0; i < mCount; i++)
							{
								Int32 status = ComparerDefault.Compare(mArrayOfItems[i], match);
								if (status == 1)
								{
									result.Add(mArrayOfItems[i]);
								}
							}
						}
						break;
					case TComparisonOperator.GreaterThanOrEqual:
						{
							for (Int32 i = 0; i < mCount; i++)
							{
								Int32 status = ComparerDefault.Compare(mArrayOfItems[i], match);
								if (status == 0 || status == 1)
								{
									result.Add(mArrayOfItems[i]);
								}
							}
						}
						break;
					default:
						break;
				}

				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка элементов по указанному предикату
			/// </summary>
			/// <param name="match">Предикатор</param>
			/// <returns>Список элементов</returns>
			//---------------------------------------------------------------------------------------------------------
			public ListArray<TItem> GetItemsWhere(Predicate<TItem> match)
			{
				ListArray<TItem> result = new ListArray<TItem>(mMaxCount);

				for (Int32 i = 0; i < mCount; i++)
				{
					if(match(mArrayOfItems[i]))
					{
						result.Add(mArrayOfItems[i]);
					}
				}

				return (result);
			}
			#endregion

			#region ======================================= ДАННЫЕ INotifyPropertyChanged =============================
			/// <summary>
			/// Событие срабатывает ПОСЛЕ изменения свойства
			/// </summary>
			public event PropertyChangedEventHandler PropertyChanged;

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации изменений свойства
			/// </summary>
			/// <param name="property_name">Имя свойства</param>
			//---------------------------------------------------------------------------------------------------------
			public void NotifyPropertyChanged(String property_name = "")
			{
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs(property_name));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации изменений свойства
			/// </summary>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			public void NotifyPropertyChanged(PropertyChangedEventArgs args)
			{
				if (PropertyChanged != null)
				{
					PropertyChanged(this, args);
				}
			}
			#endregion

			#region ======================================= ДАННЫЕ INotifyCollectionChanged ===========================
			/// <summary>
			/// Событие для информирование событий коллекций
			/// </summary>
			public event NotifyCollectionChangedEventHandler CollectionChanged;

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации изменений коллекции
			/// </summary>
			/// <param name="action">Действие с коллекцией</param>
			/// <param name="item">Элемент коллекции</param>
			/// <param name="index">Индекс элемента</param>
			//---------------------------------------------------------------------------------------------------------
			protected void NotifyCollectionChanged(NotifyCollectionChangedAction action, System.Object item, Int32 index)
			{
				if (CollectionChanged != null)
				{
					CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, item, index));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации изменений коллекции
			/// </summary>
			/// <param name="action">Действие с коллекцией</param>
			/// <param name="item">Элемент коллекции</param>
			/// <param name="index">Индекс элемента</param>
			/// <param name="old_index">Предыдущий индекс элемента</param>
			//---------------------------------------------------------------------------------------------------------
			protected void NotifyCollectionChanged(NotifyCollectionChangedAction action, System.Object item, Int32 index, Int32 old_index)
			{
				if (CollectionChanged != null)
				{
					CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, item, index, old_index));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вспомогательный метод для нотификации изменений коллекции
			/// </summary>
			/// <param name="action">Действие с коллекцией</param>
			/// <param name="old_item">Элемент коллекции</param>
			/// <param name="new_item">Элемент коллекции</param>
			/// <param name="index">Индекс элемента</param>
			//---------------------------------------------------------------------------------------------------------
			protected void NotifyCollectionChanged(NotifyCollectionChangedAction action, System.Object old_item, System.Object new_item, Int32 index)
			{
				if (CollectionChanged != null)
				{
					CollectionChanged(this, new NotifyCollectionChangedEventArgs(action, new_item, old_item, index));
				}
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================