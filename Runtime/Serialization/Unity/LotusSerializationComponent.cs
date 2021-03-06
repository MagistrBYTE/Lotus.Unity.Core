//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема сериализации Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusSerializationComponent.cs
*		Компонент сериализации данных.
*		Реализация компонента предназначенного для сохранения данных стандартных компонентов Unity и пользовательских
*	компонентов. Определяет игровой объект как сохраняемый и позволяет задать объем сохраняемых данных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using UnityEngine;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreUnitySerialization
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Компонент сериализации данных
		/// </summary>
		/// <remarks>
		/// Реализация компонента предназначенного для сохранения данных стандартных компонентов Unity и пользовательских компонентов.
		/// Определяет игровой объект как сохраняемый и позволяет задать объем сохраняемых данных
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[AddComponentMenu("Lotus/Core/Serialization/Serialization Component")]
		public class LotusSerializationComponent : MonoBehaviour, IComparable<LotusSerializationComponent>
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Данные подсистемы сериализации
			[SerializeField]
			internal Int64 mIDKeySerial;
			[SerializeField]
			internal TSerializationVolume mSerializationVolume;
			[SerializeField]
			internal Boolean mIsSerializablePrefab;
			[SerializeField]
			internal Boolean mIsSerializableChild;

			// Служебные данные
			internal Int32 mStatusLoad;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Сериализуемый ключ объекта
			/// </summary>
			/// <remarks>
			/// Должен оставаться постоянным и уникальным на всей протяженности жизни объекта.
			/// В случае если объект создается повторно (вследствие загрузки данных) то ключ должен инициализируется существующим значением
			/// </remarks>
			public Int64 IDKeySerial
			{
				get { return mIDKeySerial; }
				set
				{
					mIDKeySerial = value;
				}
			}

			/// <summary>
			/// Объем и тип сериализации данных игрового объекта
			/// </summary>
			public TSerializationVolume SerializationVolume
			{
				get { return mSerializationVolume; }
				set { mSerializationVolume = value; }
			}

			/// <summary>
			/// Статус созданного из префаба игрового объекта
			/// </summary>
			/// <remarks>
			/// Если объект создан из префаба, то не имеет смысл хранить всего компоненты. 
			/// В большинстве случаев достаточно сохранять только компонент трансформации непосредственно в атрибутах игрового объекта
			/// </remarks>
			public Boolean IsSerializablePrefab
			{
				get { return mIsSerializablePrefab; }
				set { mIsSerializablePrefab = value; }
			}

			/// <summary>
			/// Статус сохранения дочерних игровых объектов
			/// </summary>
			/// <remarks>
			/// По умолчанию сохраняется только игровой объект и определённых набор компонентов.
			/// Включение свойства означает что сохранятся вся дочерняя иерархия игровых объектов
			/// </remarks>
			public Boolean IsSerializableChild
			{
				get { return mIsSerializableChild; }
				set { mIsSerializableChild = value; }
			}

			/// <summary>
			/// Статус загрузки/обновления объекта
			/// </summary>
			public Int32 StatusLoad
			{
				get { return mStatusLoad; }
				set { mStatusLoad = value; }
			}
			#endregion

			#region ======================================= СОБЫТИЯ UNITY =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Инициализация скрипта при присоединении его к объекту(в режиме редактора)
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Reset()
			{
				Init();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Псевдоконструктор скрипта
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Awake()
			{
				Init();
				XSerializationDispatcherUnity.RegisterSerialization(this);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Игровой объект уничтожился
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void OnDestroy()
			{
				XSerializationDispatcherUnity.UnRegisterSerialization(this);
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнения двух компонентов сериализации для формирования правильного последовательности сериализации
			/// </summary>
			/// <param name="other">Компонент сериализации</param>
			/// <returns>Статус сравнения</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(LotusSerializationComponent other)
			{
				Int32 depth_this = this.transform.AbsoluteDepth();
				Int32 depth_other = other.transform.AbsoluteDepth();

				if (depth_this > depth_other)
				{
					return 1;
				}
				else
				{
					if (depth_this < depth_other)
					{
						return -1;
					}
					else
					{
						return 0;
					}
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Первичная инициализация данных и параметров компонента
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Init()
			{
				if (mIDKeySerial == -1)
				{
					mIDKeySerial = XGenerateId.Generate(this);
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