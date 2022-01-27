﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusSystemSingleton.cs
*		Паттерн Singleton.
*		Реализация паттерна Singleton применительно к системе Unity.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.01.2022
//=====================================================================================================================
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreUnity
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Режим поведения при дублировании экземпляров объектов
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public enum TSingletonDestroyMode
		{
			/// <summary>
			/// Ничего не делать
			/// </summary>
			None,

			/// <summary>
			/// Удалить полностью игровой объект на котором находиться компонент экземпляра
			/// </summary>
			GameObject,

			/// <summary>
			/// Удалить только компонент экземпляра
			/// </summary>
			Component
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Определение интерфейса паттерна Singleton
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusSingleton
		{
			/// <summary>
			/// Статус основного экземпляра
			/// </summary>
			/// <remarks>
			/// <para>
			/// Статус имеет значение только если используется несколько объектов.
			/// Например, в основной и двух дополнительный сценах присутствуют объекты. 
			/// Когда ведется работа в режиме редактора и тестируется, например, любая сцена то объект присутствует в единственном числе - это обеспечивается конструкций языка и платформой.
			/// </para>
			/// <para>
			/// Однако если мы используем концепцию при которой одна сцена всегда загружена, а остальные загружаются и выгружаются
			/// по мере необходимости, то неизбежно в приложении возникнуть два объекта.
			/// Указанной свойство как раз и позволяет учесть такой вариант и показать какой объект основной, а какой можно удалить (доступна операция зависит от свойства)
			/// </para>
			/// </remarks>
			Boolean IsMainInstance { get; set; }

			/// <summary>
			/// Статус удаления игрового объекта
			/// </summary>
			/// <remarks>
			/// При дублировании будет удалятся либо непосредственного игровой объект либо только компонент
			/// </remarks>
			TSingletonDestroyMode DestroyMode { get; set; }
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Реализация паттерна Singleton
		/// </summary>
		/// <remarks>
		/// Указанная реализация обеспечивает единство объекта на уровне приложения
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class LotusSystemSingleton<TMonoBehaviour> : MonoBehaviour where TMonoBehaviour : MonoBehaviour, ILotusSingleton
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			// Глобальный экземпляр
			internal static TMonoBehaviour mInstance;
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ СВОЙСТВА ======================================
			/// <summary>
			/// Глобальный экземпляр
			/// </summary>
			public static TMonoBehaviour Instance
			{
				get
				{
					if (mInstance == null)
					{
						CreateInstance();
					}

					return mInstance;
				}
			}

			/// <summary>
			/// Статус создания глобального экземпляра
			/// </summary>
			public static Boolean IsCreatedInstance
			{
				get
				{
					return (mInstance == null);
				}
			}
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка глобального экземпляра
			/// </summary>
			/// <param name="instances">Массив экземпляров</param>
			//---------------------------------------------------------------------------------------------------------
			protected static void SetInstance(TMonoBehaviour[] instances)
			{
				for (Int32 i = 0; i < instances.Length; i++)
				{
					// Если это не префаб
					if (!instances[i].gameObject.IsPrefab())
					{
						// Если это не основной глобальный экземпляр до мы его удаляем
						if (instances[i].IsMainInstance)
						{
							mInstance = instances[i];
						}
						else
						{
							String name_mono = typeof(TMonoBehaviour).Name;
							switch (instances[i].DestroyMode)
							{
								case TSingletonDestroyMode.None:
									{
										Debug.LogWarningFormat("Instance: {0} > 1, object: {1}", name_mono, 
											instances[i].gameObject.name);
									}
									break;
								case TSingletonDestroyMode.GameObject:
									{
										Debug.LogWarningFormat("Instance: {0} > 1, delete the object: {1}", name_mono,
											instances[i].gameObject.name);

										Destroy(instances[i].gameObject);
									}
									break;
								case TSingletonDestroyMode.Component:
									{
										Debug.LogWarningFormat("Instance: {0} > 1, delete the component: {1}", name_mono,
											instances[i].gameObject.name);

										Destroy(instances[i]);
									}
									break;
								default:
									break;
							}
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создание глобального экземпляра
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			protected static void CreateInstance()
			{
				// Если глобальный экземпляр пустой
				if (mInstance == null)
				{
					// Получаем все компоненты данного типа
					TMonoBehaviour[] instances = UnityEngine.Object.FindObjectsOfType<TMonoBehaviour>();
					if (instances.Length == 1)
					{
						// Все хорошо
						mInstance = instances[0];
						return;
					}
					else
					{
						// Почему-то компонентов много, мы не берем случай когда это сделано пользователем.
						// Такой случай может быть при загрузки другой сцены
						if (instances.Length > 1)
						{
							SetInstance(instances);
						}
					}
				}

				// Глобальный экземпляр мы до сих пор не нашли
				// Рассмотрим вариант когда игровой объект выключен
				if (mInstance == null)
				{
					// Получаем все компоненты данного типа
					TMonoBehaviour[] instances = UnityEngine.Resources.FindObjectsOfTypeAll<TMonoBehaviour>();
					if (instances.Length == 1)
					{
						// Все хорошо
						mInstance = instances[0];
						return;
					}
					else
					{
						// Почему-то компонентов много, мы не берем случай когда это сделано пользователем.
						// Такой случай может быть при загрузки другой сцены
						if (instances.Length > 1)
						{
							for (Int32 i = 0; i < instances.Length; i++)
							{
								SetInstance(instances);
							}
						}
					}
				}

				// Ну значит его точно нет
				if (mInstance == null)
				{
					Type type_singleton = typeof(TMonoBehaviour);
					GameObject go = new GameObject(type_singleton.Name.Replace("Lotus", ""), type_singleton);
					mInstance = go.GetComponent<TMonoBehaviour>();
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка и удаление дубликатов глобального экземпляра
			/// </summary>
			/// <remarks>
			/// Во время разработки глобальные экземпляры являются таковыми только в пределах активной сцены.
			/// Однако если использовать методику при которой есть главной сцена и второстепенные сцены загружаемые/выгружаемые по мере
			/// необходимости может возникнуть ситуация, когда в приложение работают сразу несколько экземпляров.
			/// Это неправильно. Для этого данный метод удаляет все дубликаты и оставляет только один - основной экземпляр 
			/// свойство которого <see cref="LotusSystemSingleton.IsMainInstance"/> равно True
			/// </remarks>
			/// <returns>Статус удаления дубликата</returns>
			//---------------------------------------------------------------------------------------------------------
			protected static Boolean CheckDublicate()
			{
				// Получаем все компоненты данного типа
				TMonoBehaviour[] instances = UnityEngine.Object.FindObjectsOfType<TMonoBehaviour>();
				if (instances.Length == 1)
				{
					// Все хорошо
					mInstance = instances[0];
					return false;
				}
				else
				{
					// Почему-то компонентов много, мы не берем случай когда это сделано пользователем.
					// Такой случай может быть при загрузки другой сцены
					if (instances.Length > 1)
					{
						SetInstance(instances);

						return true;
					}

					return false;
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