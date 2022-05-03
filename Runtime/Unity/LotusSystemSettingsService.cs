//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusSystemSettingsService.cs
*		Сервис для хранения глобальных системных и пользовательских настроек системы Lotus.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Linq;
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
		//! \addtogroup CoreUnity
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Сервис для хранения глобальных системных и пользовательских настроек системы Lotus
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class LotusSystemSettingsService : ScriptableObject
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Имя файла сервиса
			/// </summary>
			public const String SYSTEM_SETTINGS_SERVICE_NAME = "SystemSettingsService";

			/// <summary>
			/// Путь файла сервиса
			/// </summary>
			public const String SYSTEM_SETTINGS_SERVICE_PATH = XEditorSettings.ResourcesDataPath;
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			// Глобальный экземпляр
			internal static LotusSystemSettingsService mInstance;

#if UNITY_EDITOR
			internal static Type mTypeGameView;
			internal static MethodInfo mGetSizeOfMainGameView;
#endif
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ СВОЙСТВА ======================================
			/// <summary>
			/// Глобальный экземпляр сервиса
			/// </summary>
			public static LotusSystemSettingsService Instance
			{
				get
				{
					if (mInstance == null)
					{
						String path = SYSTEM_SETTINGS_SERVICE_PATH + SYSTEM_SETTINGS_SERVICE_NAME;
						mInstance = Resources.Load<LotusSystemSettingsService>(path);
					}

					if (mInstance == null)
					{
#if UNITY_EDITOR
						// Создаем сервис
						mInstance = ScriptableObject.CreateInstance<LotusSystemSettingsService>();
						mInstance.Create();

						// Создаем ресурс для хранения сервиса
						String path = SYSTEM_SETTINGS_SERVICE_PATH + SYSTEM_SETTINGS_SERVICE_NAME + ".asset";
						if (!Directory.Exists(SYSTEM_SETTINGS_SERVICE_PATH))
						{
							Directory.CreateDirectory(SYSTEM_SETTINGS_SERVICE_PATH);
						}
						UnityEditor.AssetDatabase.CreateAsset(mInstance, path);

						// Обновляем в редакторе
						UnityEditor.AssetDatabase.Refresh(UnityEditor.ImportAssetOptions.Default);
						UnityEditor.EditorUtility.DisplayDialog("System settings service successfully created", "Path\n" + path, "OK");
#endif
					}

					return mInstance;
				}
			}
			#endregion

			#region ======================================= СТАТИЧЕСКИЕ МЕТОДЫ ========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение размера окна игры редактора
			/// </summary>
			/// <returns>Размера окна</returns>
			//---------------------------------------------------------------------------------------------------------
			internal static Vector2 GetSizeOfMainGameView()
			{
#if UNITY_EDITOR
				if (mTypeGameView == null)
				{
					mTypeGameView = Type.GetType("UnityEditor.GameView,UnityEditor");
				}

				if (mGetSizeOfMainGameView == null)
				{
					mGetSizeOfMainGameView = mTypeGameView.GetMethod("GetSizeOfMainGameView", BindingFlags.NonPublic | BindingFlags.Static);
				}

				return (Vector2)mGetSizeOfMainGameView.Invoke(null, null);

#else
				return (new Vector2(Screen.width, Screen.height));
#endif
			}
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			[SerializeField]
			internal Int32 mDesignWidth;
			[SerializeField]
			internal Int32 mDesignHeight;

			// Данные в режиме редактора
#if UNITY_EDITOR
			[SerializeField]
			internal Boolean mExpandedDesign;
			[SerializeField]
#endif
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ПАРАМЕТРЫ РАЗРАБОТКИ
			//
			/// <summary>
			/// Текущая ширина экрана
			/// </summary>
			public Single ScreenWidth
			{
				get
				{
#if UNITY_EDITOR
					if (UnityEditor.EditorApplication.isPlaying)
					{
						return ((Int32)GetSizeOfMainGameView().x);
					}
					else
					{
						return (mDesignWidth);
					}
#else
					return (Screen.width);
#endif
				}
			}

			/// <summary>
			/// Текущая высота экрана
			/// </summary>
			public Single ScreenHeight
			{
				get
				{
#if UNITY_EDITOR
					if (UnityEditor.EditorApplication.isPlaying)
					{
						return ((Int32)GetSizeOfMainGameView().y);
					}
					else
					{
						return (mDesignHeight);
					}
#else
					return (Screen.height);
#endif
				}
			}

			/// <summary>
			/// Ширина экрана при разработке
			/// </summary>
			/// <remarks>
			/// Применяется для последующего вычисления актуального масштаба экран и корректировки 
			/// местоположения двухмерных элементов графического интерфейса
			/// </remarks>
			public Int32 DesignWidth
			{
				get
				{
					return (mDesignWidth);
				}
				set
                {
					mDesignWidth = value;
				}
			}

			/// <summary>
			/// Высота экрана при разработке
			/// </summary>
			/// <remarks>
			/// Применяется для последующего вычисления актуального масштаба экран и корректировки 
			/// местоположения двухмерных элементов графического интерфейса
			/// </remarks>
			public Int32 DesignHeight
			{
				get
				{
					return (mDesignHeight);
				}
				set
                {
					mDesignHeight = value;
				}
			}

			/// <summary>
			/// Масштаб ширины экрана по отношению к ширине экрана при разработке
			/// </summary>
			public Single ScaledScreenX
			{
				get
				{
#if UNITY_EDITOR
					if (UnityEditor.EditorApplication.isPlaying)
					{
						return (mDesignWidth / (Int32)GetSizeOfMainGameView().x);
					}
					else
					{
						return 1.0f;
					}
#else
					return (mDesignWidth / Screen.width);
#endif
				}
			}

			/// <summary>
			/// Масштаб высоты экрана по отношению к высоте экрана при разработке
			/// </summary>
			public Single ScaledScreenY
			{
				get
				{
#if UNITY_EDITOR
					if (UnityEditor.EditorApplication.isPlaying)
					{
						return (mDesignHeight / (Int32)GetSizeOfMainGameView().y);
					}
					else
					{
						return 1.0f;
					}
#else
					return (mDesignHeight / Screen.height);
#endif
				}
			}

			/// <summary>
			/// Средний коэффициент масштаба
			/// </summary>
			public Single ScaledScreenAverage
			{
				get
				{
					return (ScaledScreenX + ScaledScreenY) / 2;
				}
			}
			#endregion

			#region ======================================= СОБЫТИЯ UNITY =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Включение сервиса
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void OnEnable()
			{
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Первичное создание сервиса
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Create()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Первичная безопасная инициализация несериализуемых данных
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Init()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Принудительный сброс записанных данных на диск
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Flush()
			{
#if UNITY_EDITOR
				UnityEditor.EditorUtility.SetDirty(this);
#endif
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================