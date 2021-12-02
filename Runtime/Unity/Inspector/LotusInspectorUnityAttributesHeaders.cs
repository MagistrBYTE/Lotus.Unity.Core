//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема поддержки инспектора свойств
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusInspectorUnityAttributesHeaders.cs
*		Атрибуты декоративной отрисовки заголовков секций и групп свойств/полей компонентов.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 04.04.2021
//=====================================================================================================================
using System;
using UnityEngine;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreInspector
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Атрибут декоративной отрисовки заголовка секции
		/// </summary>
		/// <remarks>
		/// Реализация декоративной атрибута отрисовки заголовка секции c возможностью задать выравнивания и цвет текста заголовка
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
		public sealed class LotusHeaderSectionAttribute : PropertyAttribute
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal String mName;
			internal Color mTextColor = XUnityColor.Zero;
			internal TextAnchor mTextAlignment = TextAnchor.MiddleCenter;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя заголовка
			/// </summary>
			public String Name
			{
				get { return mName; }
				set { mName = value; }
			}

			/// <summary>
			/// Цвет текста заголовка
			/// </summary>
			public Color TextColor
			{
				get { return mTextColor; }
				set { mTextColor = value; }
			}

			/// <summary>
			/// Выравнивание текста заголовка
			/// </summary>
			public TextAnchor TextAlignment
			{
				get { return mTextAlignment; }
				set { mTextAlignment = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderSectionAttribute()
			{
				mName = "";
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderSectionAttribute(String name)
			{
				mName = name;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			/// <param name="color">Цвет текста заголовка</param>
			/// <param name="text_alignment">Выравнивание текста заголовка</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderSectionAttribute(String name, UInt32 color, TextAnchor text_alignment = TextAnchor.MiddleLeft)
			{
				mName = name;
				mTextColor = color.ToColor();
				mTextAlignment = text_alignment;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			/// <param name="color">Цвет текста заголовка</param>
			/// <param name="ord">Порядок отображения свойства</param>
			/// <param name="text_alignment">Выравнивание текста заголовка</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderSectionAttribute(String name, UInt32 color, Int32 ord, TextAnchor text_alignment = TextAnchor.MiddleLeft)
			{
				mName = name;
				mTextColor = color.ToColor();
				mTextAlignment = text_alignment;
				order = ord;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Атрибут декоративной отрисовки заголовка секции в боксе
		/// </summary>
		/// <remarks>
		/// Реализация атрибута декоративной отрисовки заголовка секции в боксе c возможностью задать выравнивания и цвет текста заголовка
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
		public sealed class LotusHeaderSectionBoxAttribute : PropertyAttribute
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal String mName;
			internal Color mTextColor = XUnityColor.Zero;
			internal TextAnchor mTextAlignment = TextAnchor.MiddleCenter;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя заголовка
			/// </summary>
			public String Name
			{
				get { return mName; }
				set { mName = value; }
			}

			/// <summary>
			/// Цвет текста заголовка
			/// </summary>
			public Color TextColor
			{
				get { return mTextColor; }
				set { mTextColor = value; }
			}

			/// <summary>
			/// Выравнивание текста заголовка
			/// </summary>
			public TextAnchor TextAlignment
			{
				get { return mTextAlignment; }
				set { mTextAlignment = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderSectionBoxAttribute()
			{
				mName = "";
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderSectionBoxAttribute(String name)
			{
				mName = name;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			/// <param name="color">Цвет текста заголовка</param>
			/// <param name="text_alignment">Выравнивание текста заголовка</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderSectionBoxAttribute(String name, UInt32 color, TextAnchor text_alignment = TextAnchor.MiddleLeft)
			{
				mName = name;
				mTextColor = color.ToColor();
				mTextAlignment = text_alignment;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			/// <param name="color">Цвет текста заголовка</param>
			/// <param name="ord">Порядок отображения свойства</param>
			/// <param name="text_alignment">Выравнивание текста заголовка</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderSectionBoxAttribute(String name, UInt32 color, Int32 ord, TextAnchor text_alignment = TextAnchor.MiddleLeft)
			{
				mName = name;
				mTextColor = color.ToColor();
				mTextAlignment = text_alignment;
				order = ord;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Атрибут декоративной отрисовки заголовка группы
		/// </summary>
		/// <remarks>
		/// Реализация декоративной атрибута отрисовки заголовка группы c возможностью задать выравнивания и цвет текста заголовка
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
		public sealed class LotusHeaderGroupAttribute : PropertyAttribute
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal String mName;
			internal Color mTextColor = XUnityColor.Zero;
			internal TextAnchor mTextAlignment = TextAnchor.MiddleLeft;
			internal Int32 mIndent;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя заголовка
			/// </summary>
			public String Name
			{
				get { return mName; }
				set { mName = value; }
			}

			/// <summary>
			/// Цвет текста заголовка
			/// </summary>
			public Color TextColor
			{
				get { return mTextColor; }
				set { mTextColor = value; }
			}

			/// <summary>
			/// Выравнивание текста заголовка
			/// </summary>
			public TextAnchor TextAlignment
			{
				get { return mTextAlignment; }
				set { mTextAlignment = value; }
			}

			/// <summary>
			/// Уровень смещения
			/// </summary>
			public Int32 Indent
			{
				get { return mIndent; }
				set { mIndent = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderGroupAttribute()
			{
				mName = "";
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderGroupAttribute(String name)
			{
				mName = name;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			/// <param name="indent">Уровень смещения</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderGroupAttribute(String name, Int32 indent)
			{
				mName = name;
				mIndent = indent;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			/// <param name="color">Цвет текста заголовка</param>
			/// <param name="text_alignment">Выравнивание текста заголовка</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderGroupAttribute(String name, UInt32 color, TextAnchor text_alignment = TextAnchor.MiddleLeft)
			{
				mName = name;
				mTextColor = color.ToColor();
				mTextAlignment = text_alignment;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			/// <param name="color">Цвет текста заголовка</param>
			/// <param name="ord">Порядок отображения свойства</param>
			/// <param name="text_alignment">Выравнивание текста заголовка</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderGroupAttribute(String name, UInt32 color, Int32 ord, TextAnchor text_alignment = TextAnchor.MiddleLeft)
			{
				mName = name;
				mTextColor = color.ToColor();
				mTextAlignment = text_alignment;
				order = ord;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Атрибут декоративной отрисовки заголовка группы в боксе
		/// </summary>
		/// <remarks>
		/// Реализация атрибута декоративной отрисовки заголовка группы в боксе c возможностью задать выравнивания и цвет текста заголовка
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
		public sealed class LotusHeaderGroupBoxAttribute : PropertyAttribute
		{
			#region ======================================= ДАННЫЕ ====================================================
			internal String mName;
			internal Color mTextColor = XUnityColor.Zero;
			internal TextAnchor mTextAlignment = TextAnchor.MiddleLeft;
			internal Int32 mIndent;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя заголовка
			/// </summary>
			public String Name
			{
				get { return mName; }
				set { mName = value; }
			}

			/// <summary>
			/// Цвет текста заголовка
			/// </summary>
			public Color TextColor
			{
				get { return mTextColor; }
				set { mTextColor = value; }
			}

			/// <summary>
			/// Выравнивание текста заголовка
			/// </summary>
			public TextAnchor TextAlignment
			{
				get { return mTextAlignment; }
				set { mTextAlignment = value; }
			}

			/// <summary>
			/// Уровень смещения
			/// </summary>
			public Int32 Indent
			{
				get { return mIndent; }
				set { mIndent = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderGroupBoxAttribute()
			{
				mName = "";
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderGroupBoxAttribute(String name)
			{
				mName = name;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			/// <param name="indent">Уровень смещения</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderGroupBoxAttribute(String name, Int32 indent)
			{
				mName = name;
				mIndent = indent;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			/// <param name="color">Цвет текста заголовка</param>
			/// <param name="text_alignment">Выравнивание текста заголовка</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderGroupBoxAttribute(String name, UInt32 color, TextAnchor text_alignment = TextAnchor.MiddleLeft)
			{
				mName = name;
				mTextColor = color.ToColor();
				mTextAlignment = text_alignment;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя заголовка</param>
			/// <param name="color">Цвет текста заголовка</param>
			/// <param name="ord">Порядок отображения свойства</param>
			/// <param name="text_alignment">Выравнивание текста заголовка</param>
			//---------------------------------------------------------------------------------------------------------
			public LotusHeaderGroupBoxAttribute(String name, UInt32 color, Int32 ord, TextAnchor text_alignment = TextAnchor.MiddleLeft)
			{
				mName = name;
				mTextColor = color.ToColor();
				mTextAlignment = text_alignment;
				order = ord;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================