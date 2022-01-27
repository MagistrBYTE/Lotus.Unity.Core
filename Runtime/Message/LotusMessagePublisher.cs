//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема сообщений
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusMessagePublisher.cs
*		Определение издателя сообщения.
*		Реализация издателя сообщения, через которого и происходит вся работа по отправки и обработки сообщений.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.01.2022
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreMessage
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		///  Определение интерфейса издателя сообщений
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public interface ILotusPublisher : ILotusNameable
		{
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Регистрация подписки на обработку сообщений
			/// </summary>
			/// <param name="message_handler">Интерфейс для обработки сообщений</param>
			//---------------------------------------------------------------------------------------------------------
			void RegisterMessageHandler(ILotusMessageHandler message_handler);

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Отмена регистрации подписки на обработку сообщений
			/// </summary>
			/// <param name="message_handler">Интерфейс для обработки сообщений</param>
			//---------------------------------------------------------------------------------------------------------
			void UnRegisterMessageHandler(ILotusMessageHandler message_handler);
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Издателя сообщения
		/// </summary>
		/// <remarks>
		/// Реализация издателя сообщений который хранит все обработчики сообщений и обеспечивает
		/// посылку сообщений.
		/// Методы издателя нужно вызывать вручную в соответствующих местах
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public class CPublisher : ILotusPublisher
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Идентификатор всех групп
			/// </summary>
			/// <remarks>
			/// Данное значение обозначает что сообщение должны обработать обработчики всех групп
			/// </remarks>
			public const String ALL_GROUP_NAME = "All";
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструирование объекта базового класса для определения аргумента сообщения
			/// </summary>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CMessageArgs ConstructorMessageArgs()
			{
				return new CMessageArgs(true);
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			protected internal String mName;
			protected internal PoolManager<CMessageArgs> mMessageArgsPools;
			protected internal SortedList<String, List<ILotusMessageHandler>> mMessageHandlers;
			protected internal List<String> mMessageGroups;
			protected internal Queue<TMessageHolder> mQueueMessages;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Имя издателя
			/// </summary>
			public String Name
			{
				get { return (mName); }
				set { mName = value; }
			}

			/// <summary>
			/// Пул объектов типа оболочки для хранения аргументов сообщения
			/// </summary>
			public PoolManager<CMessageArgs> MessageArgsPools
			{
				get { return (mMessageArgsPools); }
			}

			/// <summary>
			/// Список обработчиков сообщений(подписчиков) сгруппированных по группам
			/// </summary>
			public SortedList<String, List<ILotusMessageHandler>> MessageHandlers
			{
				get { return (mMessageHandlers); }
			}

			/// <summary>
			/// Список групп
			/// </summary>
			public List<String> MessageGroups
			{
				get { return (mMessageGroups); }
			}

			/// <summary>
			/// Очередь сообщений
			/// </summary>
			public Queue<TMessageHolder> QueueMessages
			{
				get { return (mQueueMessages); }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CPublisher()
				: this("")
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="name">Имя издателя</param>
			//---------------------------------------------------------------------------------------------------------
			public CPublisher(String name)
			{
				mName = name;
				mMessageArgsPools = new PoolManager<CMessageArgs>(100, ConstructorMessageArgs);
				mMessageHandlers = new SortedList<String, List<ILotusMessageHandler>>(10);
				mMessageGroups = new List<String>(6);
				mQueueMessages = new Queue<TMessageHolder>(100);
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusPublisher ====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Регистрация подписки на обработку сообщений
			/// </summary>
			/// <param name="message_handler">Интерфейс для обработки сообщений</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void RegisterMessageHandler(ILotusMessageHandler message_handler)
			{
				if (mMessageHandlers.ContainsKey(message_handler.MessageHandlerGroup))
				{
					mMessageHandlers[message_handler.MessageHandlerGroup].Add(message_handler);
				}
				else
				{
					List<ILotusMessageHandler> list_group_message = new List<ILotusMessageHandler>();
					list_group_message.Add(message_handler);
					mMessageHandlers.Add(message_handler.MessageHandlerGroup, list_group_message);

					// Добавляем
					mMessageGroups.Add(message_handler.MessageHandlerGroup);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Отмена регистрации подписки на обработку сообщений
			/// </summary>
			/// <param name="message_handler">Интерфейс для обработки сообщений</param>
			//---------------------------------------------------------------------------------------------------------
			public virtual void UnRegisterMessageHandler(ILotusMessageHandler message_handler)
			{
				if (mMessageHandlers.ContainsKey(message_handler.MessageHandlerGroup))
				{
					mMessageHandlers[message_handler.MessageHandlerGroup].Remove(message_handler);

					if (mMessageHandlers[message_handler.MessageHandlerGroup].Count == 0)
					{
						// Удаляем
						mMessageGroups.Remove(message_handler.MessageHandlerGroup);

						mMessageHandlers.Remove(message_handler.MessageHandlerGroup);
					}
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обработка сообщений
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void OnUpdate()
			{
				// Если у нас есть обработчики и сообщения
				if (mMessageHandlers.Count > 0 && mQueueMessages.Count > 0)
				{
					// Перебираем все сообщения
					while (mQueueMessages.Count != 0)
					{
						// Выталкиваем сообщения
						TMessageHolder message_holder = mQueueMessages.Dequeue();

						// Смотрим его назначение
						if (message_holder.Group == ALL_GROUP_NAME)
						{
							// Проходим все группы и обрабатываем сообщение
							List<ILotusMessageHandler> message_handlers;
							Int32 code = XMessageHandlerResultCode.NEGATIVE_RESULT;
							for (Int32 i = 0; i < mMessageGroups.Count; i++)
							{
								message_handlers = mMessageHandlers[mMessageGroups[i]];
								for (Int32 j = 0; j < message_handlers.Count; j++)
								{
									code = message_handlers[j].OnMessageHandler(message_holder.Message);
								}
							}

							// Сообщение почему-то обработано с отрицательным результатом 
							if (code == XMessageHandlerResultCode.NEGATIVE_RESULT)
							{
#if UNITY_2017_1_OR_NEWER
								UnityEngine.Debug.LogWarning(message_holder.GetStatusDesc(code));
#else
								XLogger.LogWarning(message_holder.GetStatusDesc(code));
#endif
							}
						}
						else
						{
							Int32 code = XMessageHandlerResultCode.NEGATIVE_RESULT;

							// Ищем целевую группу и обрабатываем сообщение
							List<ILotusMessageHandler> message_handlers;
							if (mMessageHandlers.TryGetValue(message_holder.Group, out message_handlers))
							{
								for (Int32 j = 0; j < message_handlers.Count; j++)
								{
									code = message_handlers[j].OnMessageHandler(message_holder.Message);
								}
								// Сообщение почему-то обработано с отрицательным результатом 
								if (code == XMessageHandlerResultCode.NEGATIVE_RESULT)
								{
#if UNITY_2017_1_OR_NEWER
									UnityEngine.Debug.LogWarning(message_holder.GetStatusDesc(code));
#else
									XLogger.LogWarning(message_holder.GetStatusDesc(code));
#endif
								}
							}
							else
							{
								// Такая группа не найдена
#if UNITY_2017_1_OR_NEWER
								UnityEngine.Debug.LogWarning(message_holder.GetStatusDesc(XMessageHandlerResultCode.NOT_PROCESSED));
#else
								XLogger.LogWarning(message_holder.GetStatusDesc(XMessageHandlerResultCode.NOT_PROCESSED));
#endif
							}
						}

						// Если объект был из пула
						if (message_holder.Message.IsPoolObject)
						{
							mMessageArgsPools.Release(message_holder.Message);
						}
					}
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ОТПРАВКИ СООБЩЕНИЙ =================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Послать сообщения всем
			/// </summary>
			/// <param name="message">Аргументы сообщения</param>
			//---------------------------------------------------------------------------------------------------------
			public void SendMessage(CMessageArgs message)
			{
				TMessageHolder message_holder = new TMessageHolder(ALL_GROUP_NAME, message);
				mQueueMessages.Enqueue(message_holder);
			}

#if (UNITY_2017_1_OR_NEWER)
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Послать сообщения всем
			/// </summary>
			/// <param name="name">Имя сообщения</param>
			/// <param name="data">Данные сообщения</param>
			/// <param name="sender">Источник сообщения</param>
			//---------------------------------------------------------------------------------------------------------
			public void SendMessage(String name, Single data, UnityEngine.MonoBehaviour sender)
			{
				CMessageArgs message = mMessageArgsPools.Take();
				message.Name = name;
				message.FloatValue = data;
				message.Sender = sender;
				TMessageHolder message_holder = new TMessageHolder(ALL_GROUP_NAME, message);
				mQueueMessages.Enqueue(message_holder);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Послать сообщения всем
			/// </summary>
			/// <param name="name">Имя сообщения</param>
			/// <param name="data">Данные сообщения</param>
			/// <param name="sender">Источник сообщения</param>
			//---------------------------------------------------------------------------------------------------------
			public void SendMessage(String name, UnityEngine.Vector3 data, UnityEngine.MonoBehaviour sender)
			{
				CMessageArgs message = mMessageArgsPools.Take();
				message.Name = name;
				message.Vector3DValue = data;
				message.Sender = sender;
				TMessageHolder message_holder = new TMessageHolder(ALL_GROUP_NAME, message);
				mQueueMessages.Enqueue(message_holder);
			}
#endif
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Послать сообщения определённой группе объектов
			/// </summary>
			/// <param name="group">Группа для которой отправляется сообщение</param>
			/// <param name="message">Аргументы сообщения</param>
			//---------------------------------------------------------------------------------------------------------
			public void SendMessage(String group, CMessageArgs message)
			{
				TMessageHolder message_holder = new TMessageHolder(group, message);
				mQueueMessages.Enqueue(message_holder);
			}

#if (UNITY_2017_1_OR_NEWER)
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Послать сообщения определённой группе объектов
			/// </summary>
			/// <param name="group">Группа для которой отправляется сообщение</param>
			/// <param name="name">Имя сообщения</param>
			/// <param name="sender">Источник сообщения</param>
			//---------------------------------------------------------------------------------------------------------
			public void SendMessage(String group, String name, UnityEngine.MonoBehaviour sender)
			{
				CMessageArgs message = mMessageArgsPools.Take();
				message.Name = name;
				message.Sender = sender;
				TMessageHolder message_holder = new TMessageHolder(group, message);
				mQueueMessages.Enqueue(message_holder);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Послать сообщения определённой группе объектов
			/// </summary>
			/// <param name="group">Группа для которой отправляется сообщение</param>
			/// <param name="name">Имя сообщения</param>
			/// <param name="data">Данные сообщения</param>
			/// <param name="sender">Источник сообщения</param>
			//---------------------------------------------------------------------------------------------------------
			public void SendMessage(String group, String name, Single data, UnityEngine.MonoBehaviour sender)
			{
				CMessageArgs message = mMessageArgsPools.Take();
				message.Name = name;
				message.FloatValue = data;
				message.Sender = sender;
				TMessageHolder message_holder = new TMessageHolder(group, message);
				mQueueMessages.Enqueue(message_holder);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Послать сообщения определённой группе объектов
			/// </summary>
			/// <param name="group">Группа для которой отправляется сообщение</param>
			/// <param name="name">Имя сообщения</param>
			/// <param name="data">Данные сообщения</param>
			/// <param name="sender">Источник сообщения</param>
			//---------------------------------------------------------------------------------------------------------
			public void SendMessage(String group, String name, UnityEngine.Vector3 data, UnityEngine.MonoBehaviour sender)
			{
				CMessageArgs message = mMessageArgsPools.Take();
				message.Name = name;
				message.Vector3DValue = data;
				message.Sender = sender;
				TMessageHolder message_holder = new TMessageHolder(group, message);
				mQueueMessages.Enqueue(message_holder);
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