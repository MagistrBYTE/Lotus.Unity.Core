//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема сериализации
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusSerializatorObjectXml.cs
*		Сериализация стандартных объектов в формат Xml.
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
		/// Статический класс реализующий сериализацию стандартных объектов в формат Xml
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XSerializatorObjectXml
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись члена объекта (поля/свойства) в формат XML
			/// </summary>
			/// <param name="writer">Средство записи данных в формат XML</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="member">Данные для сериализации члена объекта</param>
			/// <param name="serializer">Сериализатор</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteMemberToXml(XmlWriter writer, System.Object instance, 
				TSerializeDataMember member, CBaseSerializer serializer)
			{
				// Получаем типа члена объекта
				Type member_type = member.GetMemberType();

				switch (member.SerializeType)
				{
					case TSerializeMemberType.Primitive:
						{
							// Получаем значение члена объекта
							System.Object value = member.GetMemberValue(instance);
							XSerializatorPrimitiveXml.WriteToAttribute(writer, member_type, value, member.Name);
						}
						break;
					case TSerializeMemberType.Struct:
						{
							// Получаем значение члена объекта
							System.Object value = member.GetMemberValue(instance);
							WriteInstanceToXml(writer, value, member.Name, serializer);
						}
						break;
					case TSerializeMemberType.Class:
						{
							// Получаем значение члена объекта
							System.Object value = member.GetMemberValue(instance);
							WriteInstanceToXml(writer, value, member.Name, serializer);
						}
						break;
					case TSerializeMemberType.List:
						{
							//
							// Это коллекция
							//
							// Получаем коллекцию
							IList collection_instance = member.GetMemberValue(instance) as IList;

							// Тип элемента коллекции
							Type element_type = member_type.GetClassicCollectionItemType();

							// Записываем
							XSerializatorCollectionXml.WriteCollectionToXml(writer, element_type, collection_instance, serializer, member.Name);
						}
						break;
					case TSerializeMemberType.Dictionary:
						{
							//
							// Реализовать
							//
						}
						break;
					case TSerializeMemberType.Reference:
						{
							//
							// Реализовать
							//
						}
						break;
#if UNITY_2017_1_OR_NEWER
					case TSerializeMemberType.UnityComponent:
					case TSerializeMemberType.UnityUserComponent:
						{
							// Записываем начало элемента
							writer.WriteStartElement(member.Name);
							{
								// У нас может быть ситуация когда тип члена объекта это ссылка на базовый класс
								// а реально присвоен объект производного класса

								// Получаем объект члена данных
								System.Object value = member.GetMemberValue(instance);
								if (value != null)
								{
									// У нас реальный объект - получаем его тип
									Type current_type = value.GetType();
									writer.WriteStartElement(current_type.Name);
									{
										XSerializatorUnityXml.WriteReferenceComponentToXml(writer, value as UnityEngine.Component);
									}
									writer.WriteEndElement();
								}
								else
								{
									// Запишем пустую ссылку по типу члена объекта
									writer.WriteStartElement(member_type.Name);
									writer.WriteEndElement();
								}
							}
							writer.WriteEndElement();
						}
						break;
					case TSerializeMemberType.UnityGameObject:
						{
							// Записываем начало элемента
							writer.WriteStartElement(member.Name);
							{
								writer.WriteStartElement(nameof(UnityEngine.GameObject));
								{
									// Получаем значение члена объекта
									System.Object value = member.GetMemberValue(instance);
									XSerializatorUnityXml.WriteReferenceGameObjectToXml(writer, value as UnityEngine.GameObject);

								}
								writer.WriteEndElement();
							}
							writer.WriteEndElement();
						}
						break;
					case TSerializeMemberType.UnityResource:
					case TSerializeMemberType.UnityUserResource:
						{
							// Записываем начало элемента
							writer.WriteStartElement(member.Name);
							{
								// У нас может быть ситуация когда тип члена объекта это ссылка на базовый класс
								// а реально присвоен объект производного класса

								// Получаем значение члена объекта
								System.Object value = member.GetMemberValue(instance);
								if (value != null)
								{
									// У нас реальный объект - получаем его тип
									Type current_type = value.GetType();
									writer.WriteStartElement(current_type.Name);
									{
										XSerializatorUnityXml.WriteReferenceResourceToXml(writer, value as UnityEngine.Object);
									}
									writer.WriteEndElement();
								}
								else
								{
									// Запишем пустую ссылку по типу члена объекта
									writer.WriteStartElement(member_type.Name);
									writer.WriteEndElement();
								}
							}
							writer.WriteEndElement();
						}
						break;
#endif
					default:
						break;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись объекта в формат элемента XML
			/// </summary>
			/// <param name="writer">Средство записи данных в формат XML</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="element_name">Имя элемента</param>
			/// <param name="serializer">Сериализатор</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteInstanceToXml(XmlWriter writer, System.Object instance, 
				String element_name, CBaseSerializer serializer)
			{
				// Получаем реальный тип объекта
				Type object_type = instance.GetType();

				// Получаем данные сериализации по этому типу
				CSerializeData serialize_data = serializer.GetSerializeData(object_type);

				if (serialize_data != null)
				{
					// Записываем начало элемента
					writer.WriteStartElement(String.IsNullOrEmpty(element_name) ? serialize_data.SerializeNameType : element_name);
				}
				else
				{
					// Записываем начало элемента
					writer.WriteStartElement(String.IsNullOrEmpty(element_name) ? object_type.Name : element_name);
				}

				// Если он может сам себя записать
				if (instance is ILotusSerializeToXml)
				{
					ILotusSerializeToXml serializable_self = instance as ILotusSerializeToXml;
					serializable_self.WriteToXml(writer);
				}
				else
				{
					// Смотрим, поддерживает ли объект интерфейс сериализации
					ILotusSerializableObject serializable = instance as ILotusSerializableObject;
					if (serializable != null)
					{
						// Если поддерживает то записываем атрибут
						writer.WriteAttributeString(nameof(ILotusSerializableObject.IDKeySerial), serializable.IDKeySerial.ToString());
					}

					if (serialize_data != null && serialize_data.Members != null)
					{
						for (Int32 i = 0; i < serialize_data.Members.Count; i++)
						{
							WriteMemberToXml(writer, instance, serialize_data.Members[i], serializer);
						}
					}
				}

				// Записываем окончание элемента
				writer.WriteEndElement();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись всего объекта в формат XML
			/// </summary>
			/// <param name="writer">Средство записи данных в формат XML</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="serializer">Сериализатор</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteDataToXml(XmlWriter writer, System.Object instance, CBaseSerializer serializer)
			{
				// Получаем типа объекта
				Type object_type = instance.GetType();

				if (object_type.IsDictionaryType())
				{
					//
					// Реализовать
					//
				}
				else
				{
					if (object_type.IsClassicCollectionType())
					{
						//
						// Это коллекция
						//
						// Получаем коллекцию
						IList collection_instance = instance as IList;

						// Тип элемента коллекции
						Type element_type = object_type.GetClassicCollectionItemType();

						// Записываем
						XSerializatorCollectionXml.WriteCollectionToXml(writer, element_type, collection_instance, 
							serializer, CSerializerXml.XML_NAME_ELEMENT_COLLECTION);
					}
					else
					{
						// Получаем данные сериализации для этого объекта
						CSerializeData serialize_data = serializer.GetSerializeData(object_type);

						if (serialize_data == null)
						{
							return;
						}

						switch (serialize_data.SerializeDataType)
						{
							case TSerializeDataType.Primitive:
								{
									// Записываем начало элемента
									writer.WriteStartElement(object_type.Name);
									XSerializatorPrimitiveXml.WriteToAttribute(writer, object_type, instance,
										CSerializerXml.XML_NAME_ATTRIBUTE_VALUE);
									writer.WriteEndElement();
								}
								break;
							case TSerializeDataType.Struct:
								{
									XSerializatorObjectXml.WriteInstanceToXml(writer, instance, 
										serialize_data.SerializeNameType, serializer);
								}
								break;
							case TSerializeDataType.Class:
								{
									XSerializatorObjectXml.WriteInstanceToXml(writer, instance, 
										serialize_data.SerializeNameType, serializer);
								}
								break;
#if UNITY_2017_1_OR_NEWER
							case TSerializeDataType.UnityComponent:
							case TSerializeDataType.UnityUserComponent:
								{
									writer.WriteStartElement(object_type.Name);
									XSerializatorUnityXml.WriteReferenceComponentToXml(writer, instance as UnityEngine.Component);
									writer.WriteEndElement();
								}
								break;
							case TSerializeDataType.UnityGameObject:
								{
									writer.WriteStartElement(object_type.Name);
									XSerializatorUnityXml.WriteReferenceGameObjectToXml(writer, instance as UnityEngine.GameObject);
									writer.WriteEndElement();
								}
								break;
							case TSerializeDataType.UnityResource:
							case TSerializeDataType.UnityUserResource:
								{
									writer.WriteStartElement(object_type.Name);
									XSerializatorUnityXml.WriteReferenceResourceToXml(writer, instance as UnityEngine.Object);
									writer.WriteEndElement();
								}
								break;
#endif
							default:
								break;
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запись объекта в формат элемента XML
			/// </summary>
			/// <remarks>
			/// Оптимизированная версия предназначенная для записи списка объектов
			/// </remarks>
			/// <param name="writer">Средство записи данных в формат XML</param>
			/// <param name="serialize_data">Данные сериализации</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="serializer">Сериализатор</param>
			//---------------------------------------------------------------------------------------------------------
			public static void WriteInstanceToXml(XmlWriter writer, CSerializeData serialize_data, 
				System.Object instance, CBaseSerializer serializer)
			{
				// Получаем реальный тип объекта
				Type object_type = serialize_data.SerializeType;

				// Записываем начало элемента
				writer.WriteStartElement(object_type.Name);

				// Если он может сам себя записать
				if (instance is ILotusSerializeToXml)
				{
					ILotusSerializeToXml serializable_self = instance as ILotusSerializeToXml;
					serializable_self.WriteToXml(writer);
				}
				else
				{
					// Смотрим, поддерживает ли объект интерфейс сериализации
					ILotusSerializableObject serializable = instance as ILotusSerializableObject;
					if (serializable != null)
					{
						// Если поддерживает то записываем атрибут
						writer.WriteAttributeString(nameof(ILotusSerializableObject.IDKeySerial), serializable.IDKeySerial.ToString());
					}

					if (serialize_data != null && serialize_data.Members != null)
					{
						for (Int32 i = 0; i < serialize_data.Members.Count; i++)
						{
							WriteMemberToXml(writer, instance, serialize_data.Members[i], serializer);
						}
					}
				}

				// Записываем окончание элемента
				writer.WriteEndElement();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение члена данных (поля/свойства) из формата XML
			/// </summary>
			/// <param name="reader">Средство чтения данных формата XML</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="member">Член данных</param>
			/// <param name="serializer">Сериализатор</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ReadMemberFromXml(XmlReader reader, System.Object instance, 
				TSerializeDataMember member, CBaseSerializer serializer)
			{
				// Получаем тип члена данных
				Type member_type = member.GetMemberType();

				switch (member.SerializeType)
				{
					case TSerializeMemberType.Primitive:
						{
							// Читаем
							System.Object child_instance = XSerializatorPrimitiveXml.ReadFromAttribute(reader, member_type, member.Name);

							// Обновляем еще раз
							try
							{
								member.SetMemberValue(instance, child_instance);
							}
							catch (Exception)
							{
#if (UNITY_2017_1_OR_NEWER)
								UnityEngine.Debug.LogErrorFormat("Failed to set property <{0}> of object <{1}> to value <{2}>", member.Name, instance, child_instance);
#else
								XLogger.LogErrorFormatModule(nameof(XSerializationDispatcher), "Failed to set property <{0}> of object <{1}> to value <{2}>", member.Name, instance, child_instance);
#endif
							}
						}
						break;
					case TSerializeMemberType.Struct:
						{
							// Получаем данные сериализации для этого типа
							CSerializeData serialize_data = serializer.GetSerializeData(member_type);

							if (serialize_data == null)
							{
#if (UNITY_2017_1_OR_NEWER)
								UnityEngine.Debug.LogErrorFormat("There is no specified type: <{0}>", member_type.Name);
#else
								XLogger.LogErrorFormatModule(nameof(XSerializationDispatcher), "There is no specified type: <{0}>", member_type.Name);
#endif
								return;
							}

							// Читаем
							System.Object child_instance = ReadInstanceFromXml(reader, serialize_data, null, member.Name, serializer);

							// Обновляем еще раз
							try
							{
								member.SetMemberValue(instance, child_instance);
							}
							catch (Exception)
							{
#if (UNITY_2017_1_OR_NEWER)
								UnityEngine.Debug.LogErrorFormat("Failed to set property <{0}> of object <{1}> to value <{2}>", member.Name, instance, child_instance);
#else
								XLogger.LogErrorFormatModule(nameof(XSerializationDispatcher), "Failed to set property <{0}> of object <{1}> to value <{2}>", member.Name, instance, child_instance);
#endif
							}
						}
						break;
					case TSerializeMemberType.Class:
						{
							// Получаем данные сериализации для этого типа
							CSerializeData serialize_data = serializer.GetSerializeData(member_type);

							if (serialize_data == null)
							{
#if (UNITY_2017_1_OR_NEWER)
								UnityEngine.Debug.LogErrorFormat("There is no specified type: <{0}>", member_type.Name);
#else
								XLogger.LogErrorFormatModule(nameof(XSerializationDispatcher), "There is no specified type: <{0}>", member_type.Name);
#endif
								return;
							}

							// Читаем
							System.Object child_instance = ReadInstanceFromXml(reader, serialize_data, null, member.Name, serializer);

							// Обновляем еще раз
							try
							{
								member.SetMemberValue(instance, child_instance);
							}
							catch (Exception)
							{
#if (UNITY_2017_1_OR_NEWER)
								UnityEngine.Debug.LogErrorFormat("Failed to set property <{0}> of object <{1}> to value <{2}>", member.Name, instance, child_instance);
#else
								XLogger.LogErrorFormatModule(nameof(XSerializationDispatcher), "Failed to set property <{0}> of object <{1}> to value <{2}>", member.Name, instance, child_instance);
#endif
							}
						}
						break;
					case TSerializeMemberType.List:
						{
							//
							// Это коллекция
							//
							// Перемещаемся к элементу
							reader.MoveToElement(member.Name);

							// Читаем количество элементов
							Int32 count = reader.ReadIntegerFromAttribute(nameof(IList.Count));

							// Получаем коллекцию
							IList collection = member.GetMemberValue(instance) as IList;

							// Если ее нет то создаем и устанавливаем
							if (collection == null)
							{
								// Если это стандартный список или массив то используем конструктор с аргументом
								if (member_type.IsArray ||
									(member_type.IsGenericType && member_type.Name == XExtensionReflectionType.LIST_1) ||
									(member_type.IsGenericType && member_type.Name == XExtensionReflectionType.LIST_ARRAY_1))
								{
									collection = XReflection.CreateInstance(member_type, count > 0 ? count : 10) as IList;
								}
								else
								{
									// Используем конструктор без параметров
									collection = XReflection.CreateInstance(member_type) as IList;
								}
								member.SetMemberValue(instance, collection);
							}

							XSerializatorCollectionXml.ReadCollectionFromXml(reader, collection, member.Name, serializer, count);
						}
						break;
					case TSerializeMemberType.Dictionary:
						{
							//
							// Реализовать
							//
						}
						break;
					case TSerializeMemberType.Reference:
						{
							//
							// Реализовать
							//
						}
						break;
#if UNITY_2017_1_OR_NEWER
					case TSerializeMemberType.UnityComponent:
					case TSerializeMemberType.UnityUserComponent:
						{
							XSerializatorUnityXml.ReadReferenceComponentFromXml(reader, instance, member, serializer);
						}
						break;
					case TSerializeMemberType.UnityGameObject:
						{
							XSerializatorUnityXml.ReadReferenceGameObjectFromXml(reader, instance, member, serializer);
						}
						break;
					case TSerializeMemberType.UnityResource:
					case TSerializeMemberType.UnityUserResource:
						{
							XSerializatorUnityXml.ReadReferenceResourceFromXml(reader, instance, member, serializer);
						}
						break;
#endif
					default:
						break;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение объекта из формата элемента XML
			/// </summary>
			/// <param name="reader">Средство чтения данных формата XML</param>
			/// <param name="serialize_data">Данные сериализации</param>
			/// <param name="element_name">Имя элемента</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="serializer">Сериализатор</param>
			/// <returns>Экземпляр объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object ReadInstanceFromXml(XmlReader reader, CSerializeData serialize_data, 
				System.Object instance, String element_name, CBaseSerializer serializer)
			{
				// Перемещаемся к элементу
				reader.MoveToElement(element_name);

				Boolean is_support_serializable = false;
				Int64 id_key_serial = -1;
				Boolean is_existing_object = false;

				// Получаем тип объекта
				Type object_type = serialize_data.SerializeType;

				// Смотрим, поддерживает ли тип интерфейс сериализации
				if (instance == null)
				{
					if (object_type.IsSupportInterface<ILotusSerializableObject>())
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
								instance = serializer.SerializableObjects[id_key_serial];
								is_existing_object = true;
							}
						}
					}
				}

				// Объект мы не нашли - значит создаем с помощью конструктора
				if (instance == null)
				{
					if (serializer.Constructor == null)
					{
						instance = XReflection.CreateInstance(object_type);
					}
					else
					{
						instance = serializer.Constructor(object_type.Name);

						// Конструктор почему то не создал объект указанного типа
						if (instance == null)
						{
							instance = XReflection.CreateInstance(object_type);
						}
					}
				}

				// Если объект может сам себя прочитать
				if (instance is ILotusSerializeToXml)
				{
					ILotusSerializeToXml serializable_self = instance as ILotusSerializeToXml;
					serializable_self.ReadFromXml(reader);

					// Этот объект тоже может поддерживать сериализацию
					if (instance is ILotusSerializableObject)
					{
						is_support_serializable = true;
					}
				}
				else
				{
					// Последовательно читаем данные 
					if (serialize_data.Members != null)
					{
						for (Int32 i = 0; i < serialize_data.Members.Count; i++)
						{
							ReadMemberFromXml(reader, instance, serialize_data.Members[i], serializer);
						}
					}
				}

				// Если объект поддерживает сериализацию и является созданным
				if (is_support_serializable && is_existing_object == false)
				{
					ILotusSerializableObject serializable = instance as ILotusSerializableObject;
					serializer.SerializableObjects.Add(serializable.IDKeySerial, serializable);
				}

				return instance;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Чтение всех данных объекта из формата XML
			/// </summary>
			/// <param name="reader">Средство чтения данных формата XML</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="serializer">Сериализатор</param>
			/// <param name="index">Индекс при применении индексированных свойств</param>
			/// <returns>Экземпляр объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object ReadDataFromXml(XmlReader reader, System.Object instance, CBaseSerializer serializer, Int32 index = -1)
			{
				System.Object result = null;

				// Это словарь
				if (reader.Name == CSerializerXml.XML_NAME_ELEMENT_DICTIONARY)
				{
					//
					// Реализовать
					//
				}
				else
				{
					if (reader.Name == CSerializerXml.XML_NAME_ELEMENT_COLLECTION)
					{
						result = XSerializatorCollectionXml.ReadCollectionFromXml(reader, instance as IList, CSerializerXml.XML_NAME_ELEMENT_COLLECTION, serializer, 0);
					}
					else
					{
						// Получаем данные сериализации для этого типа
						CSerializeData serialize_data = serializer.GetSerializeData(reader.Name);

						if (serialize_data == null)
						{
#if (UNITY_2017_1_OR_NEWER)
							UnityEngine.Debug.LogErrorFormat("There is no specified type: <{0}>", reader.Name);
#else
							XLogger.LogErrorFormatModule(nameof(XSerializationDispatcher), "There is no specified type: <{0}>", reader.Name);
#endif
							return (result);
						}

						Type object_type = serialize_data.SerializeType;

						// Если тип может сам себя загрузить
						if (object_type.IsSupportInterface<ILotusSerializeToXml>())
						{
							if (instance == null)
							{
								result = XReflection.CreateInstance(object_type);
							}
							else
							{
								result = instance;
							}
							ILotusSerializeToXml serializable_self = result as ILotusSerializeToXml;
							serializable_self.ReadFromXml(reader);

							return (result);
						}

						switch (serialize_data.SerializeDataType)
						{
							case TSerializeDataType.Primitive:
								{
									result = XSerializatorPrimitiveXml.ReadFromAttribute(reader, object_type, CSerializerXml.XML_NAME_ATTRIBUTE_VALUE);
								}
								break;
							case TSerializeDataType.Struct:
								{
									result = XSerializatorObjectXml.ReadInstanceFromXml(reader, serialize_data, instance, reader.Name, serializer);
								}
								break;
							case TSerializeDataType.Class:
								{
									result = XSerializatorObjectXml.ReadInstanceFromXml(reader, serialize_data, instance, reader.Name, serializer);
								}
								break;
#if UNITY_2017_1_OR_NEWER
							case TSerializeDataType.UnityComponent:
							case TSerializeDataType.UnityUserComponent:
								{
									result = XSerializatorUnityXml.ReadReferenceComponentFromXml(reader, instance, new TSerializeDataMember(reader.Name), serializer, index);
								}
								break;
							case TSerializeDataType.UnityGameObject:
								{
									result = XSerializatorUnityXml.ReadReferenceGameObjectFromXml(reader, instance, new TSerializeDataMember(reader.Name), serializer, index);
								}
								break;
							case TSerializeDataType.UnityResource:
							case TSerializeDataType.UnityUserResource:
								{
									result = XSerializatorUnityXml.ReadReferenceResourceFromXml(reader, instance, new TSerializeDataMember(reader.Name), serializer, index);
								}
								break;
#endif
							default:
								break;
						}

					}
				}

				return (result);
			}
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================