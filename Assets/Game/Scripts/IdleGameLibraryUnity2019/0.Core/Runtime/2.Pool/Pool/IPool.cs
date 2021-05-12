﻿/****************************************************************************
 * 
 * https://github.com/thefuntastic/unity-object-pool
 * https://github.com/prime31/RecyclerKit
 * https://github.com/prime31
 * https://github.com/ihaiucom/ihaiu.PoolManager
 * https://github.com/ihaiucom?tab=repositories
 * https://github.com/Seneral/UndoPro
 * https://github.com/SpaceMadness/lunar-unity-plugin
 * https://github.com/jjcat/JumpCommand
 * https://github.com/appetizermonster/Unity3D-ActionEngine
 * http://robertpenner.com/easing/
 * http://robotacid.com/documents/code/Easing.cs
 * https://github.com/appetizermonster/Unity3D-Co/blob/master/Assets/Co/Co.cs
 * https://github.com/modesttree/Zenject
 * https://github.com/RyanNielson/awesome-unity
 * https://github.com/appetizermonster/Unity3D-RecompileDisabler
 * https://github.com/Demigiant
 * https://github.com/CatLib
 * https://github.com/ketoo/NoahGameFrame
 * https://github.com/ketoo
 * https://github.com/MFatihMAR/UnityGAME
 * https://github.com/MFatihMAR
 * https://github.com/David-Desmaisons/EasyActor
 * https://github.com/carbers/UnityComponent
 * https://github.com/mr-kelly/KSFramework
 * https://github.com/EllanJiang/GameFramework
 * https://github.com/Ourpalm/ILRuntime
 * https://github.com/Ourpalm/ILRuntimeU3D
 * https://github.com/andoowhy/EgoCS/blob/master/Ego.cs
 * https://github.com/doukasd/Unity-Components
 * https://github.com/GreatArcStudios/unitypausemenu
 * https://github.com/Thundernerd/Unity3D-ComponentAttribute
 * https://github.com/ChemiKhazi/ReorderableInspector
 * https://github.com/bkeiren/AwesomiumUnity
 * http://www.rivellomultimediaconsulting.com/unity3d-csharp-design-patterns/
 * http://www.rivellomultimediaconsulting.com/unity3d-applicant-tests-11/
 * http://www.rivellomultimediaconsulting.com/
 * http://www.rivellomultimediaconsulting.com/unity3d-mvcs-architectures-strangeioc-2/

 ****************************************************************************/

namespace QFramework
{
    public interface IPool<T>
    {
        T Allocate();

        bool Recycle(T obj);
    }
}