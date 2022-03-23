//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема параметрических объектов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusParameterItem.cs
*		Класс для представления параметра - объекта который содержит данные в формате имя=значения.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup CoreParameters Подсистема параметрических объектов
		//! Подсистема параметрических объектов обеспечивает представление и хранение информации в документоориентированном 
		//! стиле. Основной объект подсистемы — это параметрический объект который хранит список записей в формате
		//! имя=значения. При этом сама запись также может представлена в виде параметрического объекта. Это обеспечивает 
		//! представления иерархических структур данных.
		//! \ingroup Core
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение допустимых типов значения для параметра
		/// </summary>
		/// <remarks>
		/// Определение стандартных типов данных значения в контексте использования параметра.
		/// Типы значений спроектированы с учетом поддержки и реализации в современных документоориентированных СУБД.
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public enum TParameterValueType
		{
			//
			// ОСНОВНЫЕ ТИПЫ ДАННЫХ
			//
			/// <summary>
			/// Отсутствие определенного значения
			/// </summary>
			Null,

			/// <summary>
			/// Логический тип
			/// </summary>
			Boolean,

			/// <summary>
			/// Целый тип
			/// </summary>
			Integer,

			/// <summary>
			/// Вещественный тип
			/// </summary>
			Real,

			/// <summary>
			/// Тип даты-времени
			/// </summary>
			DateTime,

			/// <summary>
			/// Строковый тип
			/// </summary>
			String,

			/// <summary>
			/// Перечисление
			/// </summary>
			Enum,

			/// <summary>
			/// Список объектов определённого типа
			/// </summary>
			List,

			/// <summary>
			/// Базовый объект
			/// </summary>
			Object,

			/// <summary>
			/// Список параметрических объектов
			/// </summary>
			Parameters,

			//
			// ДОПОЛНИТЕЛЬНЫЕ ТИПЫ ДАННЫХ 
			//
			/// <summary>
			/// Цвет
			/// </summary>
			Color,

			/// <summary>
			/// Двухмерный объект данных
			/// </summary>
			Vector2D,

			/// <summary>
			/// Трехмерный объект данных
			/// </summary>
			Vector3D,

			/// <summary>
			/// Четырехмерный объект данных
			/// </summary>
			Vector4D
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение интерфейса для представления параметра - объекта который содержит данные в формате имя=значения
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface IParameterItem : ICloneable, ILotusNameable, ILotusIdentifierId, ILotusOwnedObject, 
			ILotusNotCalculation, ILotusVerified
		{
			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Тип данных значения
			/// </summary>
			TParameterValueType ValueType { get; }

			/// <summary>
			/// Значение параметра
			/// </summary>
			System.Object Value { get; set; }

			/// <summary>
			/// Активность параметра
			/// </summary>
			/// <remarks>
			/// Условная активность параметра - на усмотрение пользователя
			/// </remarks>
			Boolean IsActive { get; set; }

			/// <summary>
			/// Видимость параметра
			/// </summary>
			/// <remarks>
			/// Условная видимость параметра - на усмотрение пользователя
			/// </remarks>
			Boolean IsVisible { get; set; }

			/// <summary>
			/// Порядок отображения параметра при его представлении
			/// </summary>
			/// <remarks>
			/// Порядок отображения параметра используется при отображении в инспекторе свойств
			/// Сохраняется последние 8 бит, диапазон возможных значений: 0-255
			/// </remarks>
			Int32 OrderVisible { get; set; }

			/// <summary>
			/// Пользовательский тэг данных
			/// </summary>
			/// <remarks>
			/// Условный пользовательский тэг данных - на усмотрение пользователя.
			/// Сохраняется последние 8 бит, диапазон возможных значений: 0-255
			/// </remarks>
			Int32 UserTag { get; set; }

			/// <summary>
			/// Пользовательский тип данных
			/// </summary>
			/// <remarks>
			/// Условный пользовательский тип данных - на усмотрение пользователя.
			/// Сохраняется последние 8 бит, диапазон возможных значений: 0-255
			/// </remarks>
			Int32 UserData { get; set; }
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение интерфейса для представления параметра - объекта который содержит данные в формате имя=значения
		/// с конкретным типом данных
		/// </summary>
		/// <typeparam name="TValueType">Тип данных</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public interface IParameterItem<TValueType> : IParameterItem
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Значение параметра
			/// </summary>
			new TValueType Value { get; set; }
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Базовый класс для представления параметра - объекта который содержит данные в формате имя=значения
		/// </summary>
		/// <typeparam name="TValue">Тип значения</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public abstract class ParameterItem<TValue> : PropertyChangedBase, IParameterItem<TValue>,
			IComparable<ParameterItem<TValue>>, ILotusDuplicate<ParameterItem<TValue>>
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			//
			// Константы для информирования об изменении свойств
			//
			// Основные параметры
			protected static readonly PropertyChangedEventArgs PropertyArgsName = new PropertyChangedEventArgs(nameof(Name));
			protected static readonly PropertyChangedEventArgs PropertyArgsId = new PropertyChangedEventArgs(nameof(Id));
			protected static readonly PropertyChangedEventArgs PropertyArgsIValue = new PropertyChangedEventArgs(nameof(Value));
			protected static readonly PropertyChangedEventArgs PropertyArgsValue = new PropertyChangedEventArgs(nameof(Value));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsActive = new PropertyChangedEventArgs(nameof(IsActive));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsVisible = new PropertyChangedEventArgs(nameof(IsVisible));
			protected static readonly PropertyChangedEventArgs PropertyArgsOrderVisible = new PropertyChangedEventArgs(nameof(OrderVisible));
			protected static readonly PropertyChangedEventArgs PropertyArgsUserTag = new PropertyChangedEventArgs(nameof(UserTag));
			protected static readonly PropertyChangedEventArgs PropertyArgsUserData = new PropertyChangedEventArgs(nameof(UserData));

			// Расчеты
			protected static readonly PropertyChangedEventArgs PropertyArgsNotCalculation = new PropertyChangedEventArgs(nameof(NotCalculation));
			protected static readonly PropertyChangedEventArgs PropertyArgsIsVerified = new PropertyChangedEventArgs(nameof(IsVerified));
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal String mName;
			protected internal TValue mValue;
			protected internal Int64 mId;
			protected internal Int32 mData;

			// Расчеты
			protected internal Boolean mNotCalculation;
			protected internal Boolean mIsVerified;

			// Владелец
			internal protected ILotusOwnerObject mOwner;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Наименование параметра
			/// </summary>
			/// <remarks>
			/// Имя параметра должно быть уникальных в пределах параметрического объекта
			/// </remarks>
			[XmlAttribute]
			public String Name
			{
				get { return mName; }
				set 
				{
					mName = value;
					NotifyPropertyChanged(PropertyArgsName);
					if (mOwner != null) mOwner.OnNotifyUpdated(this, mName, nameof(Name));
				}
			}

			/// <summary>
			/// Тип данных значения
			/// </summary>
			[XmlAttribute]
			public virtual TParameterValueType ValueType
			{
				get { return TParameterValueType.Null; }
			}

			/// <summary>
			/// Значение параметра
			/// </summary>
			[XmlIgnore]
			System.Object IParameterItem.Value
			{
				get { return (mValue); }
				set 
				{
					mValue = (TValue)value;
					NotifyPropertyChanged(PropertyArgsIValue);
					if (mOwner != null) mOwner.OnNotifyUpdated(this, mValue, nameof(Value));
				}
			}

			/// <summary>
			/// Значение параметра
			/// </summary>
			[XmlElement]
			public TValue Value
			{
				get { return (mValue); }
				set
				{
					mValue = (TValue)value;
					NotifyPropertyChanged(PropertyArgsValue);
					if (mOwner != null) mOwner.OnNotifyUpdated(this, mValue, nameof(Value));
				}
			}

			/// <summary>
			/// Уникальный идентификатор параметра
			/// </summary>
			[XmlAttribute]
			public Int64 Id
			{
				get { return (mId); }
				set
				{
					mId = value;
					NotifyPropertyChanged(PropertyArgsId);
					if (mOwner != null) mOwner.OnNotifyUpdated(this, Id, nameof(Id));
				}
			}

			/// <summary>
			/// Активность параметра
			/// </summary>
			/// <remarks>
			/// Условная активность параметра - на усмотрение пользователя
			/// </remarks>
			[XmlAttribute]
			public Boolean IsActive
			{
				get { return XPacked.UnpackBoolean(mData, 0); }
				set 
				{ 
					XPacked.PackBoolean(ref mData, 0, value);
					NotifyPropertyChanged(PropertyArgsIsActive);
					if (mOwner != null) mOwner.OnNotifyUpdated(this, IsActive, nameof(IsActive));
				}
			}

			/// <summary>
			/// Видимость параметра
			/// </summary>
			/// <remarks>
			/// Условная видимость параметра - на усмотрение пользователя
			/// </remarks>
			[XmlAttribute]
			public Boolean IsVisible
			{
				get { return XPacked.UnpackBoolean(mData, 1); }
				set 
				{
					XPacked.PackBoolean(ref mData, 1, value);
					NotifyPropertyChanged(PropertyArgsIsVisible);
					if (mOwner != null) mOwner.OnNotifyUpdated(this, IsVisible, nameof(IsVisible));
				}
			}

			/// <summary>
			/// Порядок отображения параметра при его представлении
			/// </summary>
			/// <remarks>
			/// Порядок отображения параметра используется при отображении в инспекторе свойств
			/// Сохраняется последние 8 бит, диапазон возможных значений: 0-255
			/// </remarks>
			[XmlAttribute]
			public Int32 OrderVisible
			{
				get { return XPacked.UnpackInteger(mData, 2, 8); }
				set
				{
					XPacked.PackInteger(ref mData, 2, 8, value);
					NotifyPropertyChanged(PropertyArgsOrderVisible);
					if (mOwner != null) mOwner.OnNotifyUpdated(this, OrderVisible, nameof(OrderVisible));
				}
			}

			/// <summary>
			/// Пользовательский тэг данных
			/// </summary>
			/// <remarks>
			/// Условный пользовательский тэг данных - на усмотрение пользователя.
			/// Сохраняется последние 8 бит, диапазон возможных значений: 0-255
			/// </remarks>
			[XmlAttribute]
			public Int32 UserTag
			{
				get { return XPacked.UnpackInteger(mData, 10, 8); }
				set 
				{
					XPacked.PackInteger(ref mData, 10, 8, value);
					NotifyPropertyChanged(PropertyArgsUserTag);
					if (mOwner != null) mOwner.OnNotifyUpdated(this, UserTag, nameof(UserTag));
				}
			}

			/// <summary>
			/// Пользовательский тип данных
			/// </summary>
			/// <remarks>
			/// Условный пользовательский тип данных - на усмотрение пользователя.
			/// Сохраняется последние 8 бит, диапазон возможных значений: 0-255
			/// </remarks>
			[XmlAttribute]
			public Int32 UserData
			{
				get { return XPacked.UnpackInteger(mData, 18, 8); }
				set 
				{ 
					XPacked.PackInteger(ref mData, 18, 8, value);
					NotifyPropertyChanged(PropertyArgsUserData);
					if (mOwner != null) mOwner.OnNotifyUpdated(this, UserData, nameof(UserData));
				}
			}

			/// <summary>
			/// Владелец параметра
			/// </summary>
			[XmlIgnore]
			public ILotusOwnerObject IOwner
			{
				get { return mOwner; }
				set { mOwner = value; }
			}
			#endregion

			#region ======================================= СВОЙСТВА ILotusNotCalculation =============================
			/// <summary>
			/// Не учитывать параметр в расчетах
			/// </summary>
			[XmlAttribute]
			public Boolean NotCalculation
			{
				get { return (mNotCalculation); }
				set
				{
					mNotCalculation = value;
					if (mOwner != null) mOwner.OnNotifyUpdated(this, mNotCalculation, nameof(NotCalculation));
					NotifyPropertyChanged(PropertyArgsNotCalculation);
					RaiseNotCalculationChanged();
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА ILotusVerified ===================================
			/// <summary>
			/// Статус верификации параметра
			/// </summary>
			[XmlAttribute]
			public Boolean IsVerified
			{
				get { return (mIsVerified); }
				set
				{
					mIsVerified = value;
					if (mOwner != null) mOwner.OnNotifyUpdated(this, mIsVerified, nameof(IsVerified));
					NotifyPropertyChanged(PropertyArgsIsVerified);
					RaiseIsVerifiedChanged();
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected ParameterItem()
			{
				mName = "";
				mData = 0;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="parameter_name">Имя параметра</param>
			//---------------------------------------------------------------------------------------------------------
			protected ParameterItem(String parameter_name)
			{
				mName = parameter_name;
				mData = 0;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="id">Идентификатор параметра</param>
			//---------------------------------------------------------------------------------------------------------
			protected ParameterItem(Int64 id)
			{
				mName = "";
				mId = id;
				mData = 0;
			}
			#endregion
			
			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение параметров для упорядочивания
			/// </summary>
			/// <param name="other">Настройка</param>
			/// <returns>Статус сравнения параметров</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(ParameterItem<TValue> other)
			{
				return (String.CompareOrdinal(Name, other.Name));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода параметра
			/// </summary>
			/// <returns>Хеш-код параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Int32 GetHashCode()
			{
				return (this.Name.GetHashCode() ^ mId.GetHashCode());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Полное копирование параметра
			/// </summary>
			/// <returns>Копия объекта параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public Object Clone()
			{
				return (MemberwiseClone());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение дубликата объекта
			/// </summary>
			/// <returns>Дубликат объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public ParameterItem<TValue> Duplicate()
			{
				return (MemberwiseClone() as ParameterItem<TValue>);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Текстовое представление параметра</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				String result = String.Format("{0} = {1}", mName, base.ToString());
				return (result);
			}
			#endregion

			#region ======================================= СЛУЖЕБНЫЕ МЕТОДЫ СОБЫТИЙ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменения статуса расчёта набора в расчётах.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseNotCalculationChanged()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Изменения статуса верификации объекта.
			/// Метод автоматически вызывается после установки соответствующего свойства
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void RaiseIsVerifiedChanged()
			{

			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================