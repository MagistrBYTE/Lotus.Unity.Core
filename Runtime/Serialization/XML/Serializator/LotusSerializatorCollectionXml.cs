﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема сериализации
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusSerializatorCollectionXml.cs
*		Сериализация коллекций в формат Xml.
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
		/// Статический класс реализующий сериализацию коллекций в формат Xml
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XSerializatorCollectionXml
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись коллекции в формат элемента XML
			/// </summary>
			/// <param name="writer">Средство записи данных в формат XML</param>
			/// <param name="element_type">Тип элемента коллекции</param>
			/// <param name="collection">Обобщённая коллекция</param>
			/// <param name="serializer">Сериализатор</param>
			/// <param name="element_name">Имя элемента</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteCollectionToXml(XmlWriter writer, Type element_type, IList collection, 
				CBaseSerializer serializer, String element_name)
			{
				if (collection != null)
				{
					// Записываем начало элемента
					writer.WriteStartElement(element_name);

					// Количество элементов коллекции
					writer.WriteAttributeString(nameof(IList.Count), collection.Count.ToString());

					TSerializeDataType serialize_data_type = CSerializeData.ComputeSerializeDataType(element_type);

					// Проверяем на интерфейс
					if(element_type.IsInterface && serialize_data_type == TSerializeDataType.Primitive)
					{
						// Принудительно меняем тип
						serialize_data_type = TSerializeDataType.Class;
					}

					switch (serialize_data_type)
					{
						case TSerializeDataType.Primitive:
							{
								for (Int32 i = 0; i < collection.Count; i++)
								{
									writer.WriteStartElement(element_type.Name);
									XSerializatorPrimitiveXml.WriteToAttribute(writer, element_type, collection[i], 
										CSerializerXml.XML_NAME_ATTRIBUTE_VALUE);
									writer.WriteEndElement();
								}
							}
							break;
						case TSerializeDataType.Struct:
							{
								//
								// Тип коллекции фиксирован
								//
								// Получаем данные сериализации для этого типа
								CSerializeData serialize_data = serializer.GetSerializeData(element_type);

								if (serialize_data == null)
								{
#if (UNITY_2017_1_OR_NEWER)
									UnityEngine.Debug.LogErrorFormat("There is no specified type: <{0}>", element_type.Name);
#else
									XLogger.LogErrorFormatModule(nameof(XSerializationDispatcher), "There is no specified type: <{0}>", element_type.Name);
#endif
								}
								else
								{
									for (Int32 i = 0; i < collection.Count; i++)
									{
										XSerializatorObjectXml.WriteInstanceToXml(writer, serialize_data, collection[i], serializer);
									}
								}
							}
							break;
						case TSerializeDataType.Class:
							{
								//
								// Тип коллекции может быть разным
								//
								for (Int32 i = 0; i < collection.Count; i++)
								{
									XSerializatorObjectXml.WriteInstanceToXml(writer, collection[i], null, serializer);
								}
							}
							break;
#if UNITY_2017_1_OR_NEWER
						case TSerializeDataType.UnityComponent:
						case TSerializeDataType.UnityUserComponent:
							{
								//
								// Записываем ссылку
								//
								for (Int32 i = 0; i < collection.Count; i++)
								{
									writer.WriteStartElement(element_type.Name);
									XSerializatorUnityXml.WriteReferenceComponentToXml(writer, collection[i] as UnityEngine.Component);
									writer.WriteEndElement();
								}
							}
							break;
						case TSerializeDataType.UnityGameObject:
							{
								//
								// Записываем ссылку
								//
								for (Int32 i = 0; i < collection.Count; i++)
								{
									writer.WriteStartElement(element_type.Name);
									XSerializatorUnityXml.WriteReferenceGameObjectToXml(writer, collection[i] as UnityEngine.GameObject);
									writer.WriteEndElement();
								}
							}
							break;
						case TSerializeDataType.UnityResource:
						case TSerializeDataType.UnityUserResource:
							{
								//
								// Записываем ссылку
								//
								for (Int32 i = 0; i < collection.Count; i++)
								{
									writer.WriteStartElement(element_type.Name);
									XSerializatorUnityXml.WriteReferenceResourceToXml(writer, collection[i] as UnityEngine.Object);
									writer.WriteEndElement();
								}
							}
							break;
#endif
						default:
							break;
					}

					writer.WriteEndElement();
				}
				else
				{
					//
					// Всегда записываем для сохранения топологии данных
					//
					// Записываем начало элемента
					writer.WriteStartElement(element_name);

					// Количество элементов коллекции
					writer.WriteAttributeString(nameof(IList.Count), "0");

					writer.WriteEndElement();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение коллекции объектов из формата XML
			/// </summary>
			/// <param name="reader">Средство чтения данных формата XML</param>
			/// <param name="collection">Коллекции</param>
			/// <param name="element_name">Имя элемента</param>
			/// <param name="serializer">Сериализатор</param>
			/// <param name="count">Количество элементов</param>
			/// <returns>Коллекция</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IList ReadCollectionFromXml(XmlReader reader, IList collection, String element_name,
				CBaseSerializer serializer, Int32 count)
			{
				Boolean is_new_collection = false;

				// Если коллекция нет то создаем новую
				if (collection == null)
				{
					is_new_collection = true;
					collection = new ArrayList();
				}

				// Получем базовый тип элемента коллекции
				Type element_type = null;
				if (is_new_collection)
				{
					element_type = typeof(System.Object);
				}
				else
				{
					element_type = collection.GetType().GetClassicCollectionItemType();
				}

				// Если у нас коллекция существующая то получаем данные сериализации для её элемента
				CSerializeData serialize_data = null;
				if (is_new_collection)
				{
					// Мы читаем просто коллекцию элементов, пока мы не знаем какой конкретно тип её элементов
				}
				else
				{
					serialize_data = serializer.GetSerializeData(element_type);
				}

				Int32 index = 0;
				if (reader.NodeType != XmlNodeType.None)
				{
					XmlReader reader_subtree = reader.ReadSubtree();
					while (reader_subtree.Read())
					{
						// Если элемент не равен
						if (reader_subtree.NodeType == XmlNodeType.Element && reader_subtree.Name != element_name)
						{
							// Проверяем тип
							// Если имя узла не равно имени нашего типа значит это новый тип
							if (reader_subtree.Name != element_type.Name)
							{
								// Получаем данные сериализации по этому типу
								serialize_data = serializer.GetSerializeData(reader_subtree.Name);
								if (serialize_data == null)
								{
#if (UNITY_2017_1_OR_NEWER)
									UnityEngine.Debug.LogErrorFormat("There is no specified type: <{0}>", reader_subtree.Name);
#else
									XLogger.LogErrorFormatModule(nameof(XSerializationDispatcher), "There is no specified type: <{0}>", reader_subtree.Name);
#endif
									continue;
								}
								else
								{
									element_type = serialize_data.SerializeType;
								}
							}

							// Коллекция фиксированного размера - массив
							if (collection.IsFixedSize)
							{
								if (index < collection.Count)
								{
									collection.SetAt(index, XSerializatorObjectXml.ReadDataFromXml(reader_subtree, null, serializer, index));
									index++;
								}
							}
							else
							{
								collection.SetAt(index, XSerializatorObjectXml.ReadDataFromXml(reader_subtree, null, serializer, index));
								index++;
							}
						}
					}

					if (index < count)
					{
#if (UNITY_2017_1_OR_NEWER)
						UnityEngine.Debug.LogErrorFormat("Elements read less <{0}> than required <{1}>", index, count);
#else
						XLogger.LogErrorFormatModule(nameof(XSerializationDispatcher), "Elements read less <{0}> than required <{1}>", index, count);
#endif
					}

					reader_subtree.Close();
				}

				return (collection);
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================