//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема сериализации
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusSerializatorUnityXml.cs
*		Сериализация компонентов и ссылочных данных Unity в формат Xml.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.01.2022
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
		/// Статический класс реализующий сериализацию компонентов и ссылочных данных Unity в формат Xml
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XSerializatorUnityXml
		{
			#region ======================================= МЕТОДЫ ЧТЕНИЯ/ЗАПИСИ ССЫЛОК ===============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись члена объекта (поля/свойства) компонента как ссылки в формат атрибута XML
			/// </summary>
			/// <param name="writer">Средство записи данных в формат XML</param>
			/// <param name="component">Компонент</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteReferenceComponentToXml(XmlWriter writer, Component component)
			{
				if (component != null)
				{
					writer.WriteAttributeString(nameof(CSerializeReferenceUnity.Name), component.name);
					writer.WriteAttributeString(nameof(CSerializeReferenceUnity.Path), component.gameObject.GetPathScene());
					writer.WriteAttributeString(nameof(CSerializeReferenceUnity.ID), component.gameObject.GetInstanceID().ToString());
					writer.WriteAttributeString(nameof(CSerializeReferenceUnity.Tag), component.tag);
					writer.WriteAttributeString(nameof(CSerializeReferenceUnity.IsPrefab), component.gameObject.IsPrefab().ToString());
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись члена объекта (поля/свойства) игрового объекта как ссылки в формат атрибута XML
			/// </summary>
			/// <param name="writer">Средство записи данных в формат XML</param>
			/// <param name="game_object">Игровой объект</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteReferenceGameObjectToXml(XmlWriter writer, GameObject game_object)
			{
				if (game_object != null)
				{
					writer.WriteAttributeString(nameof(CSerializeReferenceUnity.Name), game_object.name);
					writer.WriteAttributeString(nameof(CSerializeReferenceUnity.Path), game_object.GetPathScene());
					writer.WriteAttributeString(nameof(CSerializeReferenceUnity.ID), game_object.GetInstanceID().ToString());
					writer.WriteAttributeString(nameof(CSerializeReferenceUnity.Tag), game_object.tag);
					writer.WriteAttributeString(nameof(CSerializeReferenceUnity.IsPrefab), game_object.IsPrefab().ToString());
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись члена объекта (поля/свойства) ресурса в формат атрибута XML
			/// </summary>
			/// <param name="writer">Средство записи данных в формат XML</param>
			/// <param name="resource">Ресурса</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteReferenceResourceToXml(XmlWriter writer, UnityEngine.Object resource)
			{
				if (resource != null)
				{
					writer.WriteAttributeString(nameof(CSerializeReferenceUnity.Name), resource.name);
					writer.WriteAttributeString(nameof(CSerializeReferenceUnity.ID), resource.GetInstanceID().ToString());
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение члена объекта (поля/свойства) компонента из формата атрибута XML
			/// </summary>
			/// <param name="reader">Средство чтения данных формата XML</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="member">Член данных</param>
			/// <param name="serializer">Сериализатор</param>
			/// <param name="index">Индекс списка</param>
			/// <returns>Найденный компонент или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object ReadReferenceComponentFromXml(XmlReader reader, System.Object instance,
				TSerializeDataMember member, CBaseSerializer serializer, Int32 index = -1)
			{
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element && reader.Name != member.Name)
					{
						// Мы перешли на новый элемент
						break;
					}
				}

				// Если атрибутов нет значит ссылка была нулевая
				if (reader.AttributeCount == 0) return null;

				// Создаем объекта для поиска ссылки
				CSerializeReferenceUnity object_link = new CSerializeReferenceUnity();
				object_link.Instance = instance;
				object_link.Index = index;
				object_link.Member = member.MemberData;
				object_link.Name = reader.ReadStringFromAttribute(nameof(CSerializeReferenceUnity.Name), "no_name");
				object_link.Path = reader.ReadStringFromAttribute(nameof(CSerializeReferenceUnity.Path), "no_path");
				object_link.TypeObject = reader.Name;
				object_link.ID = reader.ReadIntegerFromAttribute(nameof(CSerializeReferenceUnity.ID), -1);
				object_link.Tag = reader.ReadStringFromAttribute(nameof(CSerializeReferenceUnity.Tag), "Untagged");
				object_link.IsPrefab = reader.ReadBooleanFromAttribute(nameof(CSerializeReferenceUnity.IsPrefab), false);
				object_link.CodeObject = CSerializeReferenceUnity.COMPONENT;

				// Пробуем искать
				if (object_link.LinkComponent() == false)
				{
					//Не нашли значит ссылка указывает на объект который еще не создан
					serializer.SerializeReferences.Add(object_link);
					return (null);
				}
				else
				{
					return (object_link.Result);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение члена объекта (поля/свойства) игрового объекта из формата атрибута XML
			/// </summary>
			/// <param name="reader">Средство чтения данных формата XML</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="member">Член данных</param>
			/// <param name="serializer">Сериализатор</param>
			/// <param name="index">Индекс списка</param>
			/// <returns>Найденный игровой объект или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object ReadReferenceGameObjectFromXml(XmlReader reader, System.Object instance,
				TSerializeDataMember member, CBaseSerializer serializer, Int32 index = -1)
			{
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element && reader.Name != member.Name)
					{
						// Мы перешли на новый элемент
						break;
					}
				}

				// Если атрибутов нет значит ссылка была нулевая
				if (reader.AttributeCount == 0) return null;

				// Создаем объекта для поиска ссылки
				CSerializeReferenceUnity object_link = new CSerializeReferenceUnity();
				object_link.Instance = instance;
				object_link.Index = index;
				object_link.Member = member.MemberData;
				object_link.TypeObject = nameof(GameObject);
				object_link.Name = reader.ReadStringFromAttribute(nameof(CSerializeReferenceUnity.Name), "no_name");
				object_link.Path = reader.ReadStringFromAttribute(nameof(CSerializeReferenceUnity.Path), "no_path");
				object_link.ID = reader.ReadIntegerFromAttribute(nameof(CSerializeReferenceUnity.ID), -1);
				object_link.Tag = reader.ReadStringFromAttribute(nameof(CSerializeReferenceUnity.Tag), "Untagged");
				object_link.IsPrefab = reader.ReadBooleanFromAttribute(nameof(CSerializeReferenceUnity.IsPrefab), false);
				object_link.CodeObject = CSerializeReferenceUnity.GAME_OBJECT;

				// Пробуем искать
				if (object_link.LinkGameObject() == false)
				{
					//Не нашли значит ссылка указывает на объект который еще не создан
					serializer.SerializeReferences.Add(object_link);
					return (null);
				}
				else
				{
					return (object_link.Result);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение члена объекта (поля/свойства) ресурса из формата атрибута XML
			/// </summary>
			/// <param name="reader">Средство чтения данных формата XML</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="member">Член данных</param>
			/// <param name="serializer">Сериализатор</param>
			/// <param name="index">Индекс списка</param>
			/// <returns>Найденный ресурс или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object ReadReferenceResourceFromXml(XmlReader reader, System.Object instance,
				TSerializeDataMember member, CBaseSerializer serializer, Int32 index = -1)
			{
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element && reader.Name != member.Name)
					{
						// Мы перешли на новый элемент
						break;
					}
				}

				// Если атрибутов нет значит ссылка была нулевая
				if (reader.AttributeCount == 0) return null;

				// Создаем объекта для поиска ссылки
				CSerializeReferenceUnity object_link = new CSerializeReferenceUnity();
				object_link.Instance = instance;
				object_link.Index = index;
				object_link.Member = member.MemberData;
				object_link.Name = reader.ReadStringFromAttribute(nameof(CSerializeReferenceUnity.Name), "no_name");
				object_link.TypeObject = reader.Name;
				object_link.ID = reader.ReadIntegerFromAttribute(nameof(CSerializeReferenceUnity.ID), -1);
				object_link.CodeObject = CSerializeReferenceUnity.RESOURCE;

				// Пробуем искать
				if (object_link.LinkResource() == false)
				{
					//Не нашли значит ссылка указывает на объект который еще не создан
					serializer.SerializeReferences.Add(object_link);
					return null;
				}
				else
				{
					return (object_link.Result);
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ЧТЕНИЯ/ЗАПИСИ КОМПОНЕНТА ===========================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись данных компонента в формат элемента XML
			/// </summary>
			/// <param name="writer">Средство записи данных в формат XML</param>
			/// <param name="component">Компонент</param>
			/// <param name="component_type">Тип компонента</param>
			/// <param name="serializer">Сериализатор</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteComponentToXml(XmlWriter writer, UnityEngine.Component component, 
				Type component_type, CBaseSerializer serializer)
			{
				// Получаем данные сериализации для указанного типа
				CSerializeData serialize_data = serializer.GetSerializeData(component_type);

				if (serialize_data == null)
				{
					return;
				}

				// Записываем начало элемента
				writer.WriteStartElement(component_type.Name);

				// Если это стандартный компонент Unity
				if (component_type.IsUnityModule())
				{
					for (Int32 i = 0; i < serialize_data.Members.Count; i++)
					{
						XSerializatorObjectXml.WriteMemberToXml(writer, component, serialize_data.Members[i], serializer);
					}
				}
				else
				{
					// Если он может сам себя записать
					if (component is ILotusSerializeToXml)
					{
						ILotusSerializeToXml serializable_self = component as ILotusSerializeToXml;
						serializable_self.WriteToXml(writer);
					}
					else
					{
						// Смотрим, поддерживает ли объект интерфейс сериализации
						ILotusSerializableObject serializable = component as ILotusSerializableObject;
						if (serializable != null)
						{
							// Если поддерживает то записываем атрибут
							writer.WriteAttributeString(nameof(ILotusSerializableObject.IDKeySerial), serializable.IDKeySerial.ToString());
						}

						if (serialize_data.Members != null)
						{
							for (Int32 i = 0; i < serialize_data.Members.Count; i++)
							{
								XSerializatorObjectXml.WriteMemberToXml(writer, component, serialize_data.Members[i], serializer);
							}
						}
					}
				}

				// Записываем окончание элемента
				writer.WriteEndElement();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение данных компонента из формата элемента XML
			/// </summary>
			/// <param name="reader">Средство чтения данных формата XML</param>
			/// <param name="game_object">Игровой объект</param>
			/// <param name="serialize_data">Данные сериализации</param>
			/// <param name="element_name">Имя элемента</param>
			/// <param name="serializer">Сериализатор</param>
			/// <returns>Экземпляр объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public static void ReadComponentFromXml(XmlReader reader, UnityEngine.GameObject game_object, 
				CSerializeData serialize_data, String element_name, CBaseSerializer serializer)
			{
				// Перемещаемся к элементу
				reader.MoveToElement(element_name);

				// Получаем тип компонента
				Type component_type = serialize_data.SerializeType;

				// Добавляем к игровому объекту или получаем
				UnityEngine.Component component = game_object.EnsureComponent(component_type);
				if (component == null)
				{
					UnityEngine.Debug.LogErrorFormat("Component is type <{0}> == null", component_type.Name);
					return;
				}

				// Если это стандартный компонент Unity
				if (component_type.IsUnityModule())
				{
					// Последовательно читаем данные 
					for (Int32 i = 0; i < serialize_data.Members.Count; i++)
					{
						XSerializatorObjectXml.ReadMemberFromXml(reader, component, serialize_data.Members[i], serializer);
					}
				}
				else
				{
					Boolean is_support_serializable = false;
					Int64 id_key_serial = -1;
					Boolean is_existing_object = false;

					// Смотрим, поддерживает ли тип интерфейс сериализации
					if (component_type.IsSupportInterface<ILotusSerializableObject>())
					{
						is_support_serializable = true;

						// Читаем код сериализации
						id_key_serial = reader.ReadLongFromAttribute(nameof(ILotusSerializableObject.IDKeySerial), -1);
						if (id_key_serial != -1)
						{
							// Пробуем найти в словаре объектов
							if (serializer.SerializableObjects.ContainsKey(id_key_serial))
							{
								// Теперь мы просто обновляем данные объекта
								is_existing_object = true;
							}
						}
					}

					// Если объект может сам себя прочитать
					if (component is ILotusSerializeToXml)
					{
						ILotusSerializeToXml serializable_self = component as ILotusSerializeToXml;
						serializable_self.ReadFromXml(reader);
					}
					else
					{
						if (serialize_data.Members != null)
						{
							// Последовательно читаем данные 
							for (Int32 i = 0; i < serialize_data.Members.Count; i++)
							{
								XSerializatorObjectXml.ReadMemberFromXml(reader, component, serialize_data.Members[i], serializer);
							}
						}
					}

					// Если объект поддерживает сериализацию и является созданным
					if (is_support_serializable && is_existing_object == false)
					{
						ILotusSerializableObject serializable = component as ILotusSerializableObject;
						serializer.SerializableObjects.Add(serializable.IDKeySerial, serializable);
					}
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