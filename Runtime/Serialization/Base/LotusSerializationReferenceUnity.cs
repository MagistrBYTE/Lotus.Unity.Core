//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема сериализации
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusSerializationReferenceUnity.cs
*		Класс хранящий данные для связывания поля/свойства ссылочного объекта Unity.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Reflection;
using UnityEngine;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreSerialization
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс хранящий данные для связывания поля/свойства ссылочного объекта Unity
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CSerializeReferenceUnity : CSerializeReference
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			//
			// КОДЫ ОБЪЕКТОВ
			//
			/// <summary>
			/// Код игрового объекта
			/// </summary>
			public const Int32 GAME_OBJECT = 1;

			/// <summary>
			/// Код компонента
			/// </summary>
			public const Int32 COMPONENT = 2;

			/// <summary>
			/// Код ресурса
			/// </summary>
			public const Int32 RESOURCE = 3;
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			/// <summary>
			/// Имя игрового объекта
			/// </summary>
			public String Name;

			/// <summary>
			/// Полный путь игрового объекта от корня сцены
			/// </summary>
			public String Path;

			/// <summary>
			/// Тэг игрового объекта
			/// </summary>
			public String Tag;

			/// <summary>
			/// Статус ссылки на префаб
			/// </summary>
			/// <remarks>
			/// Мы можем иметь ссылку как на объект в сцене так и на объект в ресурсах.
			/// Программно - этот же тип объекта, однако его использование значительно отличается, 
			/// поэтому мы должно знать на какой тип объекта имеется ссылка
			/// </remarks>
			public Boolean IsPrefab;

			/// <summary>
			/// Сериализатор
			/// </summary>
			public CBaseSerializer Serializer;
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск и установка объекта ссылки
			/// </summary>
			/// <returns>Статус успешности связывания</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Link()
			{
				Boolean status = false;
				switch (CodeObject)
				{
#if UNITY_2017_1_OR_NEWER
					case GAME_OBJECT:
						{
							status = LinkGameObject();
						}
						break;
					case COMPONENT:
						{
							status = LinkComponent();
						}
						break;
					case RESOURCE:
						{
							status = LinkResource();
						}
						break;
#endif
					default:
						break;
				}

				return (status);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск и установка объекта ссылки на игровой объект
			/// </summary>
			/// <returns>Статус успешности связывания</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean LinkGameObject()
			{
				if (IsPrefab)
				{
					return LinkResource();
				}

				// Находим игровой объект
				GameObject game_object = XGameObjectDispatcher.Find(ID, Path, Name, Tag);
				if (game_object != null)
				{
					// Мы однозначно нашли объект
					Member.SetMemberValue(Instance, game_object, Index);
					Result = game_object;
					return true;
				}

				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск и установка объекта ссылки на компонент
			/// </summary>
			/// <returns>Статус успешности связывания</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean LinkComponent()
			{
				if (IsPrefab)
				{
					return LinkResource();
				}

				// Получаем данные сериализации по этому типу
				CSerializeData serialize_data = Serializer.GetSerializeData(TypeObject);
				if (serialize_data != null)
				{
					Type type = serialize_data.SerializeType;

					// Находим игровой объект
					GameObject game_object = XGameObjectDispatcher.Find(ID, Path, Name, Tag);

					if (game_object != null)
					{
						// Мы однозначно нашли объект
						Component component = game_object.EnsureComponent(type);
						Member.SetMemberValue(Instance, component, Index);
						Result = component;
						return true;
					}
				}
				else
				{
					Debug.LogErrorFormat("Unknown type <{0}>", TypeObject);
				}

				return false;

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск и установка объекта ссылки на ресурс
			/// </summary>
			/// <returns>Статус успешности связывания</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean LinkResource()
			{
				// Получаем данные сериализации по этому типу
				CSerializeData serialize_data = Serializer.GetSerializeData(TypeObject);
				if (serialize_data != null)
				{
					Type type = serialize_data.SerializeType;

					// Находим ресурс
					UnityEngine.Object resource = XResourcesDispatcher.Find(ID, Name, type);
					if (resource != null)
					{
						// Мы однозначно нашли объект
						Member.SetMemberValue(Instance, resource, Index);
						Result = resource;
						return (true);
					}
				}
				else
				{
					Debug.LogErrorFormat("Unknown type <{0}>", TypeObject);
				}

				return false;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================