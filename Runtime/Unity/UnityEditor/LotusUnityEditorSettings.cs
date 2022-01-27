//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusUnityEditorSettings.cs
*		Настройки касающиеся в целом редактора Unity.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.01.2022
//=====================================================================================================================
using System;
using System.Runtime.CompilerServices;
//=====================================================================================================================
// Обеспечивает дружественность для сборки редакторов
[assembly: InternalsVisibleToAttribute("Assembly-CSharp-Editor")]
[assembly: InternalsVisibleToAttribute("Lotus.Core.Unity.Editor")]
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \defgroup CoreUnityEditor Подсистема расширения функциональности редактора Unity
		//! Подсистема поддержки редактора Unity обеспечивает расширение функциональности редактора Unity и его служебных
		//! классов, является базой для построения собственных редакторов и иных вспомогательных инструментов.
		//! \ingroup CoreUnity
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для определения настроек касающиеся в целом редактора Unity
		/// </summary>
		/// <remarks>
		/// Определяет данные для местоположения директории Lotus и размещения и упорядочивания элементов меню Lotus
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public static class XEditorSettings
		{
			#region ======================================= СТАНДАРТНЫЕ ПУТИ ==========================================
			/// <summary>
			/// Базовая директория проекта
			/// </summary>
			public const String ASSETS_PATH = "Assets/";

			/// <summary>
			/// Основной путь к директории Lotus
			/// </summary>
			/// <remarks>
			/// По умолчанию директория располагается в корне проекта. Если нужно перенести в другую директорию, 
			/// здесь нужно прописать путь. Например "Assets/Plugins/"
			/// </remarks>
			public const String MainPath = "Assets/";

			/// <summary>
			/// Директория для автосохранения
			/// </summary>
			/// <remarks>
			/// Это директория для автосохранения и автозагрузки данных подсистемы сериализации
			/// </remarks>
			public const String AutoSavePath = "Assets/AutoSave";
			#endregion

			#region ======================================= БАЗОВЫЕ ПУТИ К ИСХОДНОМУ КОДУ =============================
			/// <summary>
			/// Относительный путь директории исходного кода платформы Lotus
			/// </summary>
			public const String SourcePath = MainPath + "Runtime/";

			/// <summary>
			/// Относительный путь директории исходного кода модуля Core
			/// </summary>
			public const String SourceCorePath = SourcePath + "Lotus.Core/";

			/// <summary>
			/// Относительный путь директории исходного кода модуля Math
			/// </summary>
			public const String SourceMathPath = SourcePath + "Lotus.Math/";

			/// <summary>
			/// Относительный путь директории исходного кода модуля Object3D
			/// </summary>
			public const String SourceObject3DPath = SourcePath + "Lotus.Object3D/";

			/// <summary>
			/// Относительный путь директории исходного кода набора Graphics2D
			/// </summary>
			public const String SourceGraphics2DPath = SourcePath + "Lotus.Graphics2D/";

			/// <summary>
			/// Относительный путь директории исходного кода набора VisualEffects
			/// </summary>
			public const String SourceGraphics3DPath = SourcePath + "Lotus.VisualEffects/";

			/// <summary>
			/// Относительный путь директории исходного кода набора Environment
			/// </summary>
			public const String SourceEnvironmentPath = SourcePath + "Lotus.Environment/";

			/// <summary>
			/// Относительный путь директории исходного кода набора Person
			/// </summary>
			public const String SourcePersonPath = SourcePath + "Lotus.Person/";

			/// <summary>
			/// Относительный путь директории исходного кода набора Transport
			/// </summary>
			public const String SourceTransportPath = SourcePath + "Lotus.Transport/";

			/// <summary>
			/// Относительный путь директории исходного кода модуля Service
			/// </summary>
			public const String SourceServicePath = SourcePath + "Lotus.Service/";
			#endregion

			#region ======================================= БАЗОВЫЕ ПУТИ К РЕСУРСАМ ===================================
			/// <summary>
			/// Базовая директория для загрузки ресурсов Lotus в режиме редактора
			/// </summary>
			public const String ResourcesPath = MainPath + "Resources/";

			/// <summary>
			/// Директория для загрузки аудиоресурсов Lotus в режиме редактора
			/// </summary>
			public const String ResourcesAudioPath = ResourcesPath + "Audio/";

			/// <summary>
			/// Директория для загрузки шрифтов Lotus в режиме редактора
			/// </summary>
			public const String ResourcesFontsPath = ResourcesPath + "Fonts/";

			/// <summary>
			/// Директория для загрузки моделей Lotus в режиме редактора
			/// </summary>
			public const String ResourcesModelsPath = ResourcesPath + "Models/";

			/// <summary>
			/// Директория для загрузки шейдеров Lotus в режиме редактора
			/// </summary>
			public const String ResourcesShadersPath = ResourcesPath + "Shaders/";

			/// <summary>
			/// Директория для загрузки спрайтов анимации Lotus в режиме редактора
			/// </summary>
			public const String ResourcesSpriteSheetsPath = ResourcesPath + "SpriteSheets/";

			/// <summary>
			/// Директория для загрузки текстур Lotus в режиме редактора
			/// </summary>
			public const String ResourcesTexturesPath = ResourcesPath + "Textures/";

			/// <summary>
			/// Директория для загрузки ресурсов пользовательского интерфейса Lotus в режиме редактора
			/// </summary>
			public const String ResourcesUIPath = ResourcesPath + "UI/";
			#endregion

			#region ======================================= РАЗМЕЩЕНИЕ МЕНЮ ===========================================
			/// <summary>
			/// Размещение основного меню
			/// </summary>
			/// <remarks>
			/// По умолчанию меню располагается в корне. Если его нужно перенесите в подменю, 
			/// здесь нужно прописать путь. Например "GameObject/Lotus"
			/// </remarks>
			public const String MenuPlace = "Lotus/";

			/// <summary>
			/// Последовательность в размещении меню редактора последних элементов 
			/// </summary>
			public const Int32 MenuOrderLast = 100000;
			#endregion

			#region ======================================= РАЗМЕЩЕНИЕ МЕНЮ МОДУЛЕЙ ===================================
			/// <summary>
			/// Последовательность в размещении меню редактора базового модуля (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderCore = 0;

			/// <summary>
			/// Последовательность в размещении меню редактора математического модуля (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderMath = 1000;

			/// <summary>
			/// Последовательность в размещении меню редактора модуля алгоритмов (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderAlgorithm = 2000;

			/// <summary>
			/// Последовательность в размещении меню редактора модуля для работы с 3D контентом (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderObject3D = 3000;

			/// <summary>
			/// Последовательность в размещении меню редактора набора для работы с 2D контентом (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderGraphics2D = 4000;

			/// <summary>
			/// Последовательность в размещении меню редактора набора для работы с визуальными эффектами (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderVisualEffects = 5000;

			/// <summary>
			/// Последовательность в размещении меню редактора набора для разработки внешней среды (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderEnvironment = 6000;

			/// <summary>
			/// Последовательность в размещении меню редактора набора для работы c персонажем (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderPerson = 6000;

			/// <summary>
			/// Последовательность в размещении меню редактора набора для работы c транспортом (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderTransport = 7000;

			/// <summary>
			/// Последовательность в размещении меню редактора служебного модуля (для упорядочивания)
			/// </summary>
			public const Int32 MenuOrderService = 8000;
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================