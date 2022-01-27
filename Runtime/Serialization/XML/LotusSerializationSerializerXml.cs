//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема сериализации
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusSerializationSerializerXml.cs
*		Cериализатор для сохранения/загрузки объектов в формат Xml.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.01.2022
//=====================================================================================================================
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
		/// Cериализатор для сохранения/загрузки объектов в формат Xml
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CSerializerXml : CBaseSerializer
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			//
			// ИМЕНА ЭЛЕМЕНТОВ И АТРИБУТОВ XML
			//
			/// <summary>
			/// Имя корневого элемента узла XML
			/// </summary>
			public const String XML_NAME_ELEMENT_ROOT = "Serializations";

			/// <summary>
			/// Имя элемента для записи словаря узла XML
			/// </summary>
			public const String XML_NAME_ELEMENT_DICTIONARY = "Dictionary";

			/// <summary>
			/// Имя элемента для записи коллекций узла XML
			/// </summary>
			public const String XML_NAME_ELEMENT_COLLECTION = "Collection";

			/// <summary>
			/// Имя элемента для записи коллекции моделей узла XML
			/// </summary>
			public const String XML_NAME_ELEMENT_COLLECTION_MODEL = "CollectionModel";

			/// <summary>
			/// Имя атрибута для записи данных
			/// </summary>
			public const String XML_NAME_ATTRIBUTE_VALUE = "Value";

			/// <summary>
			/// Имя для записи нулевой ссылки пользовательского класса
			/// </summary>
			public const String XML_NAME_NULL = "NullValue";

			/// <summary>
			/// Имя версии файла XML
			/// </summary>
			public readonly static Version XML_VERSION = new Version(1, 0, 0, 0);

			//
			// БАЗОВЫЕ ПУТИ
			//
#if (UNITY_2017_1_OR_NEWER)
#if UNITY_EDITOR
			/// <summary>
			/// Путь по умолчанию для сохранения/загрузки файлов
			/// </summary>
			public static String DefaultPath = XEditorSettings.AutoSavePath;
#else
			/// <summary>
			/// Путь по умолчанию для сохранения/загрузки  файлов
			/// </summary>
			public static String DefaultPath = UnityEngine.Application.persistentDataPath;
#endif
#else
			/// <summary>
			/// Путь по умолчанию для сохранения/загрузки файлов
			/// </summary>
			public static String DefaultPath = Environment.CurrentDirectory;
#endif

			//
			// БАЗОВЫЕ РАСШИРЕНИЯ
			//
			/// <summary>
			/// Расширение файла по умолчанию
			/// </summary>
			public static String DefaultExt = XFileExtension.XML_D;
			#endregion

			#region ======================================= МЕТОДЫ СОХРАНЕНИЯ =========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохранения объекта в файл
			/// </summary>
			/// <param name="file_name">Имя файла</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="parameters">Параметры сохранения</param>
			//---------------------------------------------------------------------------------------------------------
			public override void SaveTo(String file_name, System.Object instance, CParameters parameters = null)
			{
				// Формируем правильный путь
				String path = XFilePath.GetFileName(DefaultPath, file_name, DefaultExt);

				// Создаем поток для записи
				StreamWriter stream_writer = new StreamWriter(path);
				SaveTo(stream_writer, instance);
				stream_writer.Close();

#if UNITY_EDITOR
				// Обновляем в редакторе
				UnityEditor.AssetDatabase.Refresh(UnityEditor.ImportAssetOptions.Default);
				UnityEditor.EditorUtility.DisplayDialog(XFileDialog.FILE_SAVE_SUCCESSFULLY, "Path\n" + path, "OK");
#endif
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохранения объекта в строку в формате XML
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <returns>Строка в формате XML</returns>
			//---------------------------------------------------------------------------------------------------------
			public StringBuilder SaveTo(System.Object instance)
			{
				StringBuilder file_data = new StringBuilder(200);

				// Создаем поток для записи
				StringWriter string_writer = new StringWriter(file_data);

				// Сохраняем данные
				SaveTo(string_writer, instance);
				string_writer.Close();

				return (file_data);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохранения объекта в поток данных в формате XML
			/// </summary>
			/// <param name="text_writer">Средство для записи в поток строковых данных</param>
			/// <param name="instance">Экземпляр объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public void SaveTo(TextWriter text_writer, System.Object instance)
			{
#if UNITY_EDITOR
				// Только если в режиме разработки!!!
				if (UnityEditor.EditorApplication.isPlaying == false)
				{
					OnInitSerializeData();
				}
#endif
				UpdateSerializableBeforeSave();

				// Открываем файл
				XmlWriterSettings xws = new XmlWriterSettings();
				xws.Indent = true;

				XmlWriter writer = XmlWriter.Create(text_writer, xws);

				// Записываем базовые данные
				writer.WriteStartElement(XML_NAME_ELEMENT_ROOT);
				writer.WriteAttributeString(nameof(Version), XML_VERSION.ToString());

				// Смотрим что за объект
				if (instance is ILotusSerializeToXml)
				{
					ILotusSerializeToXml serializable_self = instance as ILotusSerializeToXml;
					serializable_self.WriteToXml(writer);
				}
				else
				{
					XSerializatorObjectXml.WriteDataToXml(writer, instance, this);
				}

				UpdateSerializableAfterSave();

				// Закрываем поток
				writer.WriteEndElement();
				writer.Close();
			}
			#endregion

			#region ======================================= МЕТОДЫ ЗАГРУЗКИ ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка объекта из файла
			/// </summary>
			/// <param name="file_name">Имя файла</param>
			/// <param name="parameters">Параметры загрузки</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public override System.Object LoadFrom(String file_name, CParameters parameters = null)
			{
				// Формируем правильный путь
				String path = XFilePath.GetFileName(DefaultPath, file_name, DefaultExt);

				// Открываем файл
				StringReader string_reader = new StringReader(File.ReadAllText(path));
				return LoadFrom(string_reader);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка объекта из файла
			/// </summary>
			/// <typeparam name="TResultType">Тип объекта</typeparam>
			/// <param name="file_name">Имя файла</param>
			/// <param name="parameters">Параметры загрузки</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public override TResultType LoadFrom<TResultType>(String file_name, CParameters parameters = null)
			{
				// Формируем правильный путь
				String path = XFilePath.GetFileName(DefaultPath, file_name, DefaultExt);

				// Открываем файл
				StringReader string_reader = new StringReader(File.ReadAllText(path));
				return (TResultType)LoadFrom(string_reader);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка объекта из строки в формате XML
			/// </summary>
			/// <param name="string_xml">Строка с данными в формате XML</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object LoadFromString(String string_xml)
			{
				// Открываем файл
				StringReader string_reader = new StringReader(string_xml);
				return LoadFrom(string_reader);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка объекта из потока данных
			/// </summary>
			/// <param name="text_reader">Средство для чтения из потока строковых данных</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object LoadFrom(TextReader text_reader)
			{
#if UNITY_EDITOR
				// Только если в режиме разработки!!!
				if (UnityEditor.EditorApplication.isPlaying == false)
				{
					OnInitSerializeData();
				}
#endif

#if (UNITY_2017_1_OR_NEWER)
				// Очищаем объекты для связи
				ClearSerializeReferences();
#endif
				UpdateSerializableBeforeLoad();

				// Открываем поток
				XmlReader reader = XmlReader.Create(text_reader);

				// Читаем данные
				System.Object result = null;
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						//
						// Пропускаем корневой объект
						//
						if (reader.Name == XML_NAME_ELEMENT_ROOT)
						{
							if (reader.AttributeCount > 0)
							{
								Version version = new Version(reader.GetAttribute(nameof(Version)));
								if (version > XML_VERSION)
								{
#if (UNITY_2017_1_OR_NEWER)
									UnityEngine.Debug.LogWarningFormat("Warning version: <{0}>", version.ToString());
#else
									XLogger.LogWarningFormatModule(nameof(XSerializationDispatcher), "Warning version: <{0}>", version.ToString());
#endif
								}
							}
							continue;
						}
						else
						{
							result = XSerializatorObjectXml.ReadDataFromXml(reader, null, this);
							break;
						}
					}
				}


				// Закрываем поток
				reader.Close();
				text_reader.Close();

#if (UNITY_2017_1_OR_NEWER)
				// Связываем данные
				LinkSerializeReferences();
#endif
				UpdateSerializableAfterLoad();

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка списка объектов из файла XML
			/// </summary>
			/// <param name="file_name">Имя файла</param>
			/// <returns>Список объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public List<TType> LoadListFrom<TType>(String file_name)
			{
				// Формируем правильный путь
				String path = XFilePath.GetFileName(DefaultPath, file_name, DefaultExt);

				// Открываем файл
				StringReader string_reader = new StringReader(File.ReadAllText(path));
				return (LoadListFrom<TType>(string_reader));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка списка объектов из строки в формате XML
			/// </summary>
			/// <typeparam name="TType">Тип объекта списка</typeparam>
			/// <param name="file_data">Строка с данными в формате XML</param>
			/// <returns>Список объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public List<TType> LoadListFromString<TType>(String file_data)
			{
				// Открываем файл
				StringReader string_reader = new StringReader(file_data);
				return (LoadListFrom<TType>(string_reader));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка списка объектов из потока данных
			/// </summary>
			/// <param name="text_reader">Средство для чтения из потока строковых данных</param>
			/// <returns>Список объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public List<TType> LoadListFrom<TType>(TextReader text_reader)
			{
				ArrayList array_list = LoadFrom(text_reader) as ArrayList;
				if (array_list != null)
				{
					List<TType> list = new List<TType>(array_list.Count);
					for (Int32 i = 0; i < array_list.Count; i++)
					{
						list.Add((TType)array_list[i]);
					}

					return (list);
				}

				return null;
			}
			#endregion

			#region ======================================= МЕТОДЫ ОБНОВЛЕНИЯ =========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление объекта из файла
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="file_name">Имя файла</param>
			/// <param name="parameters">Параметры обновления</param>
			//---------------------------------------------------------------------------------------------------------
			public override void UpdateFrom(System.Object instance, String file_name, CParameters parameters = null)
			{
				// Формируем правильный путь
				String path = XFilePath.GetFileName(DefaultPath, file_name, DefaultExt);

				// Открываем поток
				StringReader string_reader = new StringReader(File.ReadAllText(path));
				UpdateFrom(instance, string_reader);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление объекта из строки в формате XML
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="file_data">Строка с данными в формате XML</param>
			//---------------------------------------------------------------------------------------------------------
			public void UpdateFromString(System.Object instance, String file_data)
			{
				// Открываем поток
				StringReader string_reader = new StringReader(file_data);
				UpdateFrom(instance, string_reader);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление объект из потока данных в формате XML
			/// </summary>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="text_reader">Средство для чтения из потока строковых данных</param>
			//---------------------------------------------------------------------------------------------------------
			public void UpdateFrom(System.Object instance, TextReader text_reader)
			{
#if UNITY_EDITOR
				// Только если в режиме разработки!!!
				if (UnityEditor.EditorApplication.isPlaying == false)
				{
					OnInitSerializeData();
				}
#endif

#if (UNITY_2017_1_OR_NEWER)
				// Очищаем объекты для связи
				ClearSerializeReferences();
#endif

				// Открываем поток
				XmlReader reader = XmlReader.Create(text_reader);
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						//
						// Пропускаем корневой объект
						//
						if (reader.Name == XML_NAME_ELEMENT_ROOT)
						{
							if (reader.AttributeCount > 0)
							{
								Version version = new Version(reader.GetAttribute(nameof(Version)));
								if (version > XML_VERSION)
								{
#if (UNITY_2017_1_OR_NEWER)
									UnityEngine.Debug.LogWarningFormat("Warning version: <{0}>", version.ToString());
#else
									XLogger.LogWarningFormatModule(nameof(XSerializationDispatcher), "Warning version: <{0}>", version.ToString());
#endif
								}
							}
							continue;
						}
						else
						{
							XSerializatorObjectXml.ReadDataFromXml(reader, instance, this);
							break;
						}
					}
				}

				// Закрываем поток
				reader.Close();
				text_reader.Close();

#if (UNITY_2017_1_OR_NEWER)
				// Связываем данные
				LinkSerializeReferences();
#endif
				UpdateSerializableAfterLoad();
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================