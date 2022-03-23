﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Базовая подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusBaseSurfaceData.cs
*		Определения поверхности как двухмерного универсального массива данных.
*		Реализация класса поверхности как двухмерного массива данных. Может заполнятся различным образом, используется
*	как некая абстракция изображения. Основной свойство то что при изменении его размеров данные могут быть интерполированы.
*	В Unity в основном планируется использовать в режиме редактора.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Linq;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreBase
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс реализующий поверхность как двухмерный массив данных
		/// </summary>
		/// <remarks>
		/// Одна из основных функций поверхности - интерполяция данных при изменении ее размеров
		/// </remarks>
		/// <typeparam name="TType">Тип данных поверхности</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		public class SurfaceData<TType> where TType : struct
		{
			#region ======================================= ДАННЫЕ ====================================================
			protected internal TType[] mData;
			protected internal Int32 mWidth;
			protected internal Int32 mHeight;
			protected internal Int32 mRank;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Данные поверхности
			/// </summary>
			public TType[] Data
			{
				get { return mData; }
			}

			/// <summary>
			/// Количество элементов в поверхности
			/// </summary>
			public Int32 Count
			{
				get { return mData.Length; }
			}

			/// <summary>
			/// Ширина поверхности при двухмерной организации данных
			/// </summary>
			public Int32 Width
			{
				get { return mWidth; }
				set { mWidth = value; }
			}

			/// <summary>
			/// Высота поверхности при двухмерной организации данных
			/// </summary>
			public Int32 Height
			{
				get { return mHeight; }
				set { mHeight = value; }
			}

			/// <summary>
			/// Размерность поверхности
			/// </summary>
			public Int32 Rank
			{
				get { return mRank; }
				set { mRank = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные поверхности предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public SurfaceData()
			{
				mData = new TType[] { default(TType) };
				mWidth = 1;
				mHeight = 1;
				mRank = 1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные поверхности указанными значениями
			/// </summary>
			/// <param name="data">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public SurfaceData(TType[] data)
			{
				mData = new TType[data.Length];
				for (Int32 i = 0; i < mData.Length; i++)
				{
					mData[i] = data[i];
				}
				mRank = 1;
			}
			#endregion

			#region ======================================= ИНДЕКСАТОР ================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Индексация элементов поверхности
			/// </summary>
			/// <param name="index">Индекс элемента</param>
			/// <returns>Элемент поверхности</returns>
			//---------------------------------------------------------------------------------------------------------
			public TType this[Int32 index]
			{
				get
				{
					return mData[index];
				}
				set
				{
					mData[index] = value;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Индексация элементов поверхности при двухмерной организации
			/// </summary>
			/// <param name="x">Индекс элемента по x</param>
			/// <param name="y">Индекс элемента по y</param>
			/// <returns>Элемент поверхности</returns>
			//---------------------------------------------------------------------------------------------------------
			public TType this[Int32 x, Int32 y]
			{
				get
				{
					return mData[x + y * mWidth];
				}
				set
				{
					mData[x + y * mWidth] = value;
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка данных
			/// </summary>
			/// <param name="count">Количество элементов</param>
			/// <param name="save_old_data">Сохранять ли старые данные</param>
			//---------------------------------------------------------------------------------------------------------
			public void Set(Int32 count, Boolean save_old_data)
			{
				if (save_old_data)
				{
					TType[] data = new TType[count];
					Int32 count_copy = count;
					if (count > Count)
					{
						count_copy = Count;
					}
					for (Int32 i = 0; i < count_copy; i++)
					{
						data[i] = mData[i];
					}

					mData = data;
				}
				else
				{
					mData = new TType[count];
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка данных из источника
			/// </summary>
			/// <param name="data">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetFromData(TType[] data)
			{
				mData = new TType[data.Length];
				for (Int32 i = 0; i < mData.Length; i++)
				{
					mData[i] = data[i];
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Линейная интерполяция значений поверхности
			/// </summary>
			/// <param name="new_width">Новая ширина</param>
			/// <param name="new_height">Новая высота</param>
			//---------------------------------------------------------------------------------------------------------
			public void Resize(Int32 new_width, Int32 new_height)
			{
				TType[] temp_data = new TType[new_width * new_height];

				Double factor_x = (Double)mWidth / (Double)new_width;
				Double factor_y = (Double)mHeight / (Double)new_height;

				for (Int32 x = 0; x < new_width; ++x)
				{
					for (Int32 y = 0; y < new_height; ++y)
					{
						Int32 gx = (Int32)Math.Floor(x * factor_x);
						Int32 gy = (Int32)Math.Floor(y * factor_y);
						TType val = this[gx, gy];
						temp_data[x + y * new_width] = val;
					}
				}

				mData = temp_data;
				mWidth = new_width;
				mHeight = new_height;
				mRank = 2;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Поверхность данных для целых значения
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CSurfaceInt : SurfaceData<Int32>
		{
			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные поверхности предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CSurfaceInt()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные поверхности указанными значениями
			/// </summary>
			/// <param name="data">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public CSurfaceInt(Int32[] data)
			{
				SetFromData(data);
				mRank = 1;
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Линейная интерполяция значений поверхности
			/// </summary>
			/// <param name="new_width">Новая ширина</param>
			/// <param name="new_height">Новая высота</param>
			/// <param name="use_bilinear">Использовать билинейную интерполяцию</param>
			//---------------------------------------------------------------------------------------------------------
			public void Resize(Int32 new_width, Int32 new_height, Boolean use_bilinear)
			{
				Int32[] temp_data = new Int32[new_width * new_height];

				Double factor_x = (Double)mWidth / (Double)new_width;
				Double factor_y = (Double)mHeight / (Double)new_height;

				if (use_bilinear)
				{
					Double fraction_x, fraction_y, one_minus_x, one_minus_y;
					Int32 ceil_x, ceil_y, floor_x, floor_y;
					Int32 c1, c2, c3, c4;
					Int32 b1, b2;

					for (Int32 x = 0; x < new_width; ++x)
					{
						for (Int32 y = 0; y < new_height; ++y)
						{
							// Setup
							floor_x = (Int32)Math.Floor(x * factor_x);
							floor_y = (Int32)Math.Floor(y * factor_y);

							ceil_x = floor_x + 1;
							if (ceil_x >= mWidth)
							{
								ceil_x = floor_x;
							}

							ceil_y = floor_y + 1;
							if (ceil_y >= mHeight)
							{
								ceil_y = floor_y;
							}

							fraction_x = x * factor_x - floor_x;
							fraction_y = y * factor_y - floor_y;
							one_minus_x = 1.0 - fraction_x;
							one_minus_y = 1.0 - fraction_y;

							c1 = this[floor_x, floor_y];
							c2 = this[ceil_x, floor_y];
							c3 = this[floor_x, ceil_y];
							c4 = this[ceil_x, ceil_y];

							b1 = (Int32)(one_minus_x * c1 + fraction_x * c2);
							b2 = (Int32)(one_minus_x * c3 + fraction_x * c4);
							Int32 val = (Int32)(one_minus_y * (Double)b1 + fraction_y * (Double)b2);


							temp_data[x + y * new_width] = val;
						}
					}
				}
				else
				{
					for (Int32 x = 0; x < new_width; ++x)
					{
						for (Int32 y = 0; y < new_height; ++y)
						{
							Int32 gx = (Int32)Math.Floor(x * factor_x);
							Int32 gy = (Int32)Math.Floor(y * factor_y);
							Int32 val = this[gx, gy];
							temp_data[x + y * new_width] = val;
						}
					}
				}

				mData = temp_data;
				mWidth = new_width;
				mHeight = new_height;
				mRank = 2;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Поверхность данных для цвета
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CSurfaceColor : SurfaceData<TColor>
		{
			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные поверхности предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CSurfaceColor()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные поверхности указанными значениями
			/// </summary>
			/// <param name="data">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public CSurfaceColor(TColor[] data)
			{
				SetFromData(data);
				mRank = 1;
			}

#if (UNITY_2017_1_OR_NEWER)
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные поверхности указанными значениями
			/// </summary>
			/// <param name="data">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public CSurfaceColor(UnityEngine.Color32[] data)
			{
				SetFromData(data);
				mRank = 1;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует данные поверхности указанными значениями
			/// </summary>
			/// <param name="data">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public CSurfaceColor(UnityEngine.Texture2D texture)
			{
				if (texture != null)
				{
					this.SetFromData(texture.GetPixels32());
					mWidth = texture.width;
					mHeight = texture.height;
					mRank = 2;
				}
				else
				{
					mData = new TColor[] { TColor.White };
					mWidth = 1;
					mHeight = 1;
					mRank = 1;
				}
			}
#endif
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
#if (UNITY_2017_1_OR_NEWER)
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка данных из источника
			/// </summary>
			/// <param name="data">Данные</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetFromData(UnityEngine.Color32[] data)
			{
				mData = new TColor[data.Length];
				for (Int32 i = 0; i < mData.Length; i++)
				{
					mData[i].A = data[i].a;
					mData[i].R = data[i].r;
					mData[i].G = data[i].g;
					mData[i].B = data[i].b;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка данных из источника (текстуры)
			/// </summary>
			/// <param name="texture">Текстура</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetFromData(UnityEngine.Texture2D texture)
			{
				if (texture != null)
				{
					this.SetFromData(texture.GetPixels32());
					mWidth = texture.width;
					mHeight = texture.height;
					mRank = 2;
				}
			}
#endif
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Линейная интерполяция значений поверхности
			/// </summary>
			/// <param name="new_width">Новая ширина</param>
			/// <param name="new_height">Новая высота</param>
			/// <param name="use_bilinear">Использовать билинейную интерполяцию</param>
			//---------------------------------------------------------------------------------------------------------
			public void Resize(Int32 new_width, Int32 new_height, Boolean use_bilinear)
			{
				TColor[] temp_data = new TColor[new_width * new_height];

				Single factor_x = (Single)mWidth / (Single)new_width;
				Single factor_y = (Single)mHeight / (Single)new_height;

				if (use_bilinear)
				{
					Single fraction_x, fraction_y, one_minus_x, one_minus_y;
					Int32 ceil_x, ceil_y, floor_x, floor_y;
					TColor c1, c2, c3, c4;
					TColor b1, b2;

					for (Int32 x = 0; x < new_width; ++x)
					{
						for (Int32 y = 0; y < new_height; ++y)
						{
							// Setup
							floor_x = (Int32)Math.Floor(x * factor_x);
							floor_y = (Int32)Math.Floor(y * factor_y);

							ceil_x = floor_x + 1;
							if (ceil_x >= mWidth)
							{
								ceil_x = floor_x;
							}

							ceil_y = floor_y + 1;
							if (ceil_y >= mHeight)
							{
								ceil_y = floor_y;
							}

							fraction_x = x * factor_x - floor_x;
							fraction_y = y * factor_y - floor_y;
							one_minus_x = 1.0f - fraction_x;
							one_minus_y = 1.0f - fraction_y;

							c1 = this[floor_x, floor_y];
							c2 = this[ceil_x, floor_y];
							c3 = this[floor_x, ceil_y];
							c4 = this[ceil_x, ceil_y];

							b1 = TColor.Add(TColor.Scale(c1, one_minus_x), TColor.Scale(c2, fraction_x));
							b2 = TColor.Add(TColor.Scale(c3, one_minus_x), TColor.Scale(c4, fraction_x));
							TColor val = TColor.Add(TColor.Scale(b1, one_minus_y), TColor.Scale(b2, fraction_y));

							temp_data[x + y * new_width] = val;
						}
					}
				}
				else
				{
					for (Int32 x = 0; x < new_width; ++x)
					{
						for (Int32 y = 0; y < new_height; ++y)
						{
							Int32 gx = (Int32)Math.Floor(x * factor_x);
							Int32 gy = (Int32)Math.Floor(y * factor_y);
							TColor val = this[gx, gy];
							temp_data[x + y * new_width] = val;
						}
					}
				}

				mData = temp_data;
				mWidth = new_width;
				mHeight = new_height;
				mRank = 2;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================