//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема сериализации
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusSerializationSerializer.cs
*		Определение концепции сериализатор данных.
*		Сериализатор данных непосредственно осуществляет процесс сохранения/загрузки данных из определённого формата данных.
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
		/// Интерфейс сериализатор данных
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusSerializer : ILotusNameable
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохранения объекта в файл
			/// </summary>
			/// <remarks>
			/// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция
			/// </remarks>
			/// <param name="file_name">Имя файла</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="parameters">Параметры сохранения</param>
			//---------------------------------------------------------------------------------------------------------
			void SaveTo(String file_name, System.Object instance, CParameters parameters = null);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка объекта из файла
			/// </summary>
			/// <remarks>
			/// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция
			/// </remarks>
			/// <param name="file_name">Имя файла</param>
			/// <param name="parameters">Параметры загрузки</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public System.Object LoadFrom(String file_name, CParameters parameters = null);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка объекта из файла
			/// </summary>
			/// <remarks>
			/// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция
			/// </remarks>
			/// <typeparam name="TResultType">Тип объекта</typeparam>
			/// <param name="file_name">Имя файла</param>
			/// <param name="parameters">Параметры загрузки</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			TResultType LoadFrom<TResultType>(String file_name, CParameters parameters = null);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление объекта из файла
			/// </summary>
			/// <remarks>
			/// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция
			/// </remarks>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="file_name">Имя файла</param>
			/// <param name="parameters">Параметры обновления</param>
			//---------------------------------------------------------------------------------------------------------
			void UpdateFrom(System.Object instance, String file_name, CParameters parameters = null);
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Базовый сериализатор
		/// </summary>
		/// <remarks>
		/// Базовый сериализатор реализует только механизм получения данных для сериализации
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public class CBaseSerializer : ILotusSerializer
		{
			#region ======================================= ДАННЫЕ ====================================================
			protected internal String mName;
			protected internal Func<String, System.Object> mConstructor;
			protected internal Dictionary<Int64, ILotusSerializableObject> mSerializableObjects;
			protected internal Dictionary<String, CSerializeData> mSerializeDataByName;

#if UNITY_2017_1_OR_NEWER
			protected internal List<CSerializeReferenceUnity> mSerializeReferences;
#endif
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя сериализатора
			/// </summary>
			public String Name
			{
				get { return (mName); }
				set { mName = value; }
			}

			/// <summary>
			/// Конструктор для создания объекта по имени типа
			/// </summary>
			public virtual Func<String, System.Object> Constructor
			{
				get
				{
					return (mConstructor);
				}
				set
				{
					mConstructor = value;
				}
			}

			/// <summary>
			/// Словарь всех объектов поддерживающих интерфейс сериализации объекта
			/// </summary>
			public virtual Dictionary<Int64, ILotusSerializableObject> SerializableObjects
			{
				get
				{
					return (mSerializableObjects);
				}
			}

			/// <summary>
			/// Словарь данных сериализации по имени типа
			/// </summary>
			public virtual Dictionary<String, CSerializeData> SerializeDataByName
			{
				get
				{
					return (mSerializeDataByName);
				}
			}

#if UNITY_2017_1_OR_NEWER
			/// <summary>
			/// Список объект для связывания данных
			/// </summary>
			public virtual List<CSerializeReferenceUnity> SerializeReferences
			{
				get
				{
					return (mSerializeReferences);
				}
			}
#endif
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CBaseSerializer()
				: this("")
			{
				
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя сериализатора</param>
			//---------------------------------------------------------------------------------------------------------
			public CBaseSerializer(String name)
			{
				mName = name;
				mSerializableObjects = new Dictionary<Int64, ILotusSerializableObject>(100);
				mSerializeDataByName = new Dictionary<String, CSerializeData>(100);
#if UNITY_2017_1_OR_NEWER
				mSerializeReferences = new List<CSerializeReferenceUnity>();
#endif
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusSerializer ===================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохранения объекта в файл
			/// </summary>
			/// <remarks>
			/// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция
			/// </remarks>
			/// <param name="file_name">Имя файла</param>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="parameters">Параметры сохранения</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void SaveTo(String file_name, System.Object instance, CParameters parameters = null)
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка объекта из файла
			/// </summary>
			/// <remarks>
			/// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция
			/// </remarks>
			/// <param name="file_name">Имя файла</param>
			/// <param name="parameters">Параметры загрузки</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual System.Object LoadFrom(String file_name, CParameters parameters = null)
			{
				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка объекта из файла
			/// </summary>
			/// <remarks>
			/// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция
			/// </remarks>
			/// <typeparam name="TResultType">Тип объекта</typeparam>
			/// <param name="file_name">Имя файла</param>
			/// <param name="parameters">Параметры загрузки</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual TResultType LoadFrom<TResultType>(String file_name, CParameters parameters = null)
			{
				return (default);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление объекта из файла
			/// </summary>
			/// <remarks>
			/// Под файлом может пониматься как собственно физический файл на диске так и любая иная концепция
			/// </remarks>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="file_name">Имя файла</param>
			/// <param name="parameters">Параметры обновления</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void UpdateFrom(System.Object instance, String file_name, CParameters parameters = null)
			{
			}
			#endregion

			#region ======================================= МЕТОДЫ ОБРАБОТКИ ДАННЫХ ===================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Первичная инициализация данных сериализации
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void OnInitSerializeData()
			{
				if (mSerializeDataByName == null)
				{
					// 1) Используем профилирование
					System.Diagnostics.Stopwatch profiler = new System.Diagnostics.Stopwatch();
					profiler.Start();

					Int32 count_assemblies = 0;
					Int32 count_types = 0;

					// Создаем словарь по имени типа (не полное имя)
					mSerializeDataByName = new Dictionary<String, CSerializeData>(300);
					// 
					mSerializeDataByName.Add(nameof(Boolean), CSerializeData.CreateNoMembers(typeof(Boolean)));
					mSerializeDataByName.Add(nameof(Byte), CSerializeData.CreateNoMembers(typeof(Byte)));
					mSerializeDataByName.Add(nameof(Char), CSerializeData.CreateNoMembers(typeof(Char)));
					mSerializeDataByName.Add(nameof(Int16), CSerializeData.CreateNoMembers(typeof(Int16)));
					mSerializeDataByName.Add(nameof(UInt16), CSerializeData.CreateNoMembers(typeof(UInt16)));
					mSerializeDataByName.Add(nameof(Int32), CSerializeData.CreateNoMembers(typeof(Int32)));
					mSerializeDataByName.Add(nameof(UInt32), CSerializeData.CreateNoMembers(typeof(UInt32)));
					mSerializeDataByName.Add(nameof(Int64), CSerializeData.CreateNoMembers(typeof(Int64)));
					mSerializeDataByName.Add(nameof(UInt64), CSerializeData.CreateNoMembers(typeof(UInt64)));
					mSerializeDataByName.Add(nameof(Single), CSerializeData.CreateNoMembers(typeof(Single)));
					mSerializeDataByName.Add(nameof(Double), CSerializeData.CreateNoMembers(typeof(Double)));
					mSerializeDataByName.Add(nameof(Decimal), CSerializeData.CreateNoMembers(typeof(Decimal)));
					mSerializeDataByName.Add(nameof(String), CSerializeData.CreateNoMembers(typeof(String)));
					mSerializeDataByName.Add(nameof(DateTime), CSerializeData.CreateNoMembers(typeof(DateTime)));
					mSerializeDataByName.Add(nameof(TimeSpan), CSerializeData.CreateNoMembers(typeof(TimeSpan)));
					mSerializeDataByName.Add(nameof(Version), CSerializeData.CreateNoMembers(typeof(Version)));
					mSerializeDataByName.Add(nameof(Uri), CSerializeData.CreateNoMembers(typeof(Uri)));

					// Получаем все загруженные сборки в домене
					var assemblies = AppDomain.CurrentDomain.GetAssemblies();

					// Проходим по всем сборкам
					for (Int32 ia = 0; ia < assemblies.Length; ia++)
					{
						// Сборка
						var assemble = assemblies[ia];

						// Если она поддерживается
						if (CheckSupportAssembly(assemble))
						{
							count_assemblies++;
							//UnityEngine.Debug.Log("Assembly: " + assemble.FullName);

							// Получаем все типы в сборке
							var types = assemble.GetExportedTypes();

							// Проходим по всем типам
							for (Int32 it = 0; it < types.Length; it++)
							{
								// Получаем тип
								var type = types[it];

								// Только он поддерживается
								if (CheckSupportType(type))
								{
									count_types++;
									//UnityEngine.Debug.Log("Type: " + type.Name);

									// Кэшируем данные
									GetSerializeDataForType(type);

									// Получаем вложенные типы
									var nested_types = type.GetNestedTypes();

									if (nested_types != null && nested_types.Length > 0)
									{
										// Анализируем внутренние типы
										for (Int32 itn = 0; itn < nested_types.Length; itn++)
										{
											var nested_type = nested_types[itn];
											if (nested_type.IsPublic)
											{
												// Кэшируем данные
												GetSerializeDataForType(type);
											}
										}
									}
								}
							}
						}
					}

					profiler.Stop();

#if UNITY_2017_1_OR_NEWER
					UnityEngine.Debug.LogFormat("Assemblies load count: {0}", count_assemblies);
					UnityEngine.Debug.LogFormat("Types load count: {0}", count_types);
					UnityEngine.Debug.LogFormat("Loaded time: {0} ms", profiler.ElapsedMilliseconds);
#else
					XLogger.LogInfoFormatModule(nameof(XSerializationDispatcher), "Assemblies load count: {0}", count_assemblies);
					XLogger.LogInfoFormatModule(nameof(XSerializationDispatcher), "Types load count: {0}", count_types);
					XLogger.LogInfoFormatModule(nameof(XSerializationDispatcher), "Loaded time: {0} ms", profiler.ElapsedMilliseconds);
#endif
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка указанной сборки на целесообразность получать типы для которых потребуются данные сериализации
			/// </summary>
			/// <param name="assembly">Сборка</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			protected virtual Boolean CheckSupportAssembly(Assembly assembly)
			{
				if (assembly.FullName.IndexOf("SyntaxTree") > -1) return (false);
				if (assembly.FullName.IndexOf("NUnit") > -1) return (false);
				if (assembly.FullName.IndexOf("Mono") > -1) return (false);
				if (assembly.FullName.IndexOf("mscorlib") > -1) return (false);
				if (assembly.FullName.IndexOf("System") > -1) return (false);
				if (assembly.FullName.IndexOf("Editor") > -1) return (false);
				if (assembly.FullName.IndexOf("Test") > -1) return (false);

				return (true);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка типа на возможность получение оптимальных(нужных) данных сериализации 
			/// </summary>
			/// <param name="type">Тип</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			protected virtual Boolean CheckSupportType(Type type)
			{
				// Небольшое ускорения
				// Перечисления всегда подлежат сериализации
				if (type.IsEnum && type.IsPublic) return (true);
				if (type.IsPrimitive && type.IsPublic) return (true);

				// Общие ограничения
				if (type.IsAbstract) return (false);
				if (type.IsGenericType) return (false);
				if (type.IsPublic == false) return (false);
				if (type.IsStaticType()) return (false);
				if (type.IsSubclassOf(typeof(Exception))) return (false);
				if (type.IsSubclassOf(typeof(Attribute))) return (false);

				//
				// Ограничения платформы Lotus
				//
				// Общие
				if (type.GetAttribute<LotusSerializeDisableAttribute>() != null) return (false);

				//
				// Ограничения Unity
				//
#if UNITY_2017_1_OR_NEWER
				// Общие
				// Типы которые принципиально не возможно сериализовать
				if (type.IsSubclassOf(typeof(UnityEngine.YieldInstruction))) return (false);
				if (type.IsSubclassOf(typeof(UnityEngine.Events.UnityEventBase))) return (false);
				if (type.IsSubclassOf(typeof(UnityEngine.EventSystems.AbstractEventData))) return (false);

				// Внутри платформы
				if (type.IsUnityModule())
				{
					if (type.FullName.Contains("UIElements")) return (false);
					if (type.FullName.IndexOf("Experimental") > -1) return (false);
					if (type.FullName.IndexOf("NUnit") > -1) return (false);
					if (type.FullName.IndexOf("SyntaxTree") > -1) return (false);
				}
				else
				{
					// Типы которые не обозначены для сериализации (за исключения перечислений)
					if (type.GetAttribute<SerializableAttribute>() == null && !type.IsEnum) return (false);
				}
#endif
				return (true);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение данных сериализации непосредственно от указанного типа
			/// </summary>
			/// <param name="type">Тип</param>
			/// <returns>Данные сериализации</returns>
			//---------------------------------------------------------------------------------------------------------
			protected virtual CSerializeData GetSerializeDataFromType(Type type)
			{
				CSerializeData serialize_data = null;

				if (type.GetAttribute<LotusSerializeDataAttribute>() != null)
				{
					try
					{
						// Есть метод который может дать конкретные данные для сериализации данного типа
						MethodInfo method = type.GetMethod(LotusSerializeDataAttribute.GET_SERIALIZE_DATA, 
							BindingFlags.Static | BindingFlags.Public);
						if (method != null)
						{
							serialize_data = (CSerializeData)method.Invoke(null, null);
						}
						else
						{
#if (UNITY_2017_1_OR_NEWER)
							UnityEngine.Debug.LogErrorFormat("SerializeData attribute, method none of type: <{0}>", type.Name);
#else
							XLogger.LogErrorFormatModule(nameof(XSerializationDispatcher), "SerializeData attribute, method none of type: <{0}>", 
								type.Name + ">");
#endif
						}
					}
					catch (Exception)
					{
					}
				}

				return (serialize_data);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение данных сериализации для типа
			/// </summary>
			/// <param name="type">Тип</param>
			/// <returns>Данные сериализации</returns>
			//---------------------------------------------------------------------------------------------------------
			protected virtual CSerializeData GetSerializeDataForType(Type type)
			{
				CSerializeData serialize_data = null;

				// Вычисляем тип данных
				TSerializeDataType serialize_data_type = CSerializeData.ComputeSerializeDataType(type);

				switch (serialize_data_type)
				{
					case TSerializeDataType.Primitive:
						{
							if (!mSerializeDataByName.ContainsKey(type.Name))
							{
								serialize_data = CSerializeData.CreateNoMembers(type);
								mSerializeDataByName.Add(type.Name, serialize_data);
							}
							break;
						}
					case TSerializeDataType.Struct:
						{
#if UNITY_2017_1_OR_NEWER
							// Структура Unity
							if (type.IsUnityModule())
							{
								// Только по имени типа
								if (!mSerializeDataByName.ContainsKey(type.Name))
								{
									serialize_data = CSerializeData.CreateForUnityStructType(type);
									if (serialize_data != null)
									{
										mSerializeDataByName.Add(type.Name, serialize_data);
									}
								}
								break;
							}
#endif
							// Структура
							if (!mSerializeDataByName.ContainsKey(type.Name))
							{
								// Пробуем получить данные непосредственно от типа 
								serialize_data = GetSerializeDataFromType(type);
								if (serialize_data != null)
								{
									mSerializeDataByName.Add(type.Name, serialize_data);
									AddSerializeDataFromAliasName(serialize_data);
								}
								else
								{
									// Вычисляем автоматические 
									serialize_data = CSerializeData.CreateForUserStructType(type);
									if (serialize_data != null)
									{
										mSerializeDataByName.Add(type.Name, serialize_data);
										AddSerializeDataFromAliasName(serialize_data);
									}
								}
							}
							break;
						}
					case TSerializeDataType.Class:
						{
#if UNITY_2017_1_OR_NEWER
							// Класс Unity
							if (type.IsUnityModule())
							{
								// Только по имени типа
								if (!mSerializeDataByName.ContainsKey(type.Name))
								{
									serialize_data = CSerializeData.CreateForUnityClassType(type);
									if (serialize_data != null)
									{
										mSerializeDataByName.Add(type.Name, serialize_data);
									}
								}
								break;
							}
#endif
							// Класс
							if (!mSerializeDataByName.ContainsKey(type.Name))
							{
								// Пробуем получить данные непосредственно от типа 
								serialize_data = GetSerializeDataFromType(type);
								if (serialize_data != null)
								{
									mSerializeDataByName.Add(type.Name, serialize_data);
									AddSerializeDataFromAliasName(serialize_data);
								}
								else
								{
									// Вычисляем автоматические 
									serialize_data = CSerializeData.CreateForUserClassType(type);
									if (serialize_data != null)
									{
										mSerializeDataByName.Add(type.Name, serialize_data);
										AddSerializeDataFromAliasName(serialize_data);
									}
								}
							}
							break;
						}
#if (UNITY_2017_1_OR_NEWER)
					case TSerializeDataType.UnityComponent:
						{
							// Только по имени типа
							if (!mSerializeDataByName.ContainsKey(type.Name))
							{
								serialize_data = CSerializeData.CreateForUnityComponentType(type);
								if (serialize_data != null)
								{
									mSerializeDataByName.Add(type.Name, serialize_data);
								}
							}
						}
						break;
					case TSerializeDataType.UnityUserComponent:
						{
							if (!mSerializeDataByName.ContainsKey(type.Name))
							{
								serialize_data = CSerializeData.CreateForUserClassType(type);
								if (serialize_data != null)
								{
									mSerializeDataByName.Add(type.Name, serialize_data);
								}
							}
						}
						break;
					case TSerializeDataType.UnityGameObject:
						{
							if (!mSerializeDataByName.ContainsKey(type.Name))
							{
								serialize_data = CSerializeData.CreateNoMembers(type);
								mSerializeDataByName.Add(type.Name, serialize_data);
							}
						}
						break;
					case TSerializeDataType.UnityResource:
					case TSerializeDataType.UnityUserResource:
						{
							if (!mSerializeDataByName.ContainsKey(type.Name))
							{
								serialize_data = CSerializeData.CreateNoMembers(type);
								mSerializeDataByName.Add(type.Name, serialize_data);
							}
						}
						break;
#endif
					default:
						break;
				}

				if (serialize_data != null)
				{
					serialize_data.SerializeDataType = serialize_data_type;
				}

				return (serialize_data);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Добавление данных для сериализации с учетом псевдонима имени типа
			/// </summary>
			/// <param name="serialize_data">Данные сериализации</param>
			//---------------------------------------------------------------------------------------------------------
			protected virtual void AddSerializeDataFromAliasName(CSerializeData serialize_data)
			{
				// Если еще есть и псевдоним имени типа 
				if (serialize_data.AliasNameType.IsExists())
				{
					// Проверяем его так возможно есть дубликаты
					if (mSerializeDataByName.ContainsKey(serialize_data.AliasNameType))
					{
						CSerializeData sd = mSerializeDataByName[serialize_data.AliasNameType];
#if (UNITY_2017_1_OR_NEWER)
						UnityEngine.Debug.LogErrorFormat("Alias <{0}> type <{1}> already exists in the type <{2}>",
						serialize_data.AliasNameType, serialize_data.SerializeType.Name, sd.SerializeType.Name);
#else
						XLogger.LogErrorFormatModule(nameof(XSerializationDispatcher), "Alias <{0}> type <{1}> already exists in the type <{2}>",
						serialize_data.AliasNameType, serialize_data.SerializeType.Name, sd.SerializeType.Name);
#endif
					}
					else
					{
						// Добавляем
						mSerializeDataByName.Add(serialize_data.AliasNameType, serialize_data);
					}
				}
			}
			#endregion

			#region ======================================= ПОЛУЧЕНИЕ ДАННЫХ СЕРИАЛИЗАЦИИ =============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение данных сериализации для указанного типа
			/// </summary>
			/// <typeparam name="TType">Тип</typeparam>
			/// <returns>Данные сериализации</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CSerializeData GetSerializeData<TType>()
			{
				return GetSerializeData(typeof(TType));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение данных сериализации для указанного типа
			/// </summary>
			/// <param name="type">Тип</param>
			/// <returns>Данные сериализации</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CSerializeData GetSerializeData(Type type)
			{
				if (SerializeDataByName.ContainsKey(type.Name))
				{
					return mSerializeDataByName[type.Name];
				}

				return (GetSerializeDataForType(type));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение данных сериализации для указанного имени типа
			/// </summary>
			/// <param name="type_name">Имя типа</param>
			/// <returns>Данные сериализации</returns>
			//---------------------------------------------------------------------------------------------------------
			public virtual CSerializeData GetSerializeData(String type_name)
			{
				if (SerializeDataByName.ContainsKey(type_name))
				{
					return mSerializeDataByName[type_name];
				}

#if (UNITY_2017_1_OR_NEWER)
				UnityEngine.Debug.LogErrorFormat("Not serialize data of type <{0}>", type_name);
#else
				XLogger.LogErrorFormatModule(nameof(XSerializationDispatcher), "Not serialize data of type <{0}>", type_name);
#endif

				return null;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка данных сериализации
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void ClearSerializeMembers()
			{
				foreach (var item in SerializeDataByName.Values)
				{
					item.ClearMembers();
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ОБЪЕКТОВ СЕРИАЛИЗАЦИИ ==============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление объектов поддерживающих интерфейс сериализации перед сохранением
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void UpdateSerializableBeforeSave()
			{
				if (SerializableObjects.Count > 0)
				{
					CParameters parameters = new CParameters( new CParameterObject(this.Name, this));
					foreach (var item in SerializableObjects.Values)
					{
						if (item is ILotusBeforeSave before_save)
						{
							before_save.OnBeforeSave(parameters);
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление объектов поддерживающих интерфейс сериализации после сохранения
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void UpdateSerializableAfterSave()
			{
				if (SerializableObjects.Count > 0)
				{
					CParameters parameters = new CParameters(new CParameterObject(this.Name, this));
					foreach (var item in SerializableObjects.Values)
					{
						if (item is ILotusAfterSave after_save)
						{
							after_save.OnAfterSave(parameters);
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление объектов поддерживающих интерфейс сериализации перед загрузкой
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void UpdateSerializableBeforeLoad()
			{
				if (SerializableObjects.Count > 0)
				{
					CParameters parameters = new CParameters(new CParameterObject(this.Name, this));
					foreach (var item in SerializableObjects.Values)
					{
						if (item is ILotusBeforeLoad before_load)
						{
							before_load.OnBeforeLoad(parameters);
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление объектов поддерживающих интерфейс сериализации после полной загрузки
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void UpdateSerializableAfterLoad()
			{
				if (SerializableObjects.Count > 0)
				{
					CParameters parameters = new CParameters(new CParameterObject(this.Name, this));
					foreach (var item in SerializableObjects.Values)
					{
						if (item is ILotusAfterLoad after_load)
						{
							after_load.OnAfterLoad(parameters);
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка словаря объектов поддерживающих интерфейс сериализации
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void ClearSerializableObjects()
			{
				SerializableObjects.Clear();
			}
			#endregion

			#region ======================================= МЕТОДЫ ДЛЯ СВЯЗЫВАНИЯ ДАННЫХ ==============================
#if UNITY_2017_1_OR_NEWER
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Связывание ссылочных данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void LinkSerializeReferences()
			{
				for (Int32 i = 0; i < mSerializeReferences.Count; i++)
				{
					mSerializeReferences[i].Link();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Очистка ссылочных данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public virtual void ClearSerializeReferences()
			{
				mSerializeReferences.Clear();
			}
#endif
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================