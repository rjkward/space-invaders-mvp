using System;
using System.Collections.Generic;
using SpaceInvadersMVP.Util.Enum;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Installer {
    [CreateAssetMenu(fileName = "MainMenuUIInstaller", menuName = "Installers/MainMenuUIInstaller")]
    public class MainMenuUIInstaller : UIInstaller<MainMenuState>
    {
    }
}
