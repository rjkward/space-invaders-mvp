using System;
using System.Collections.Generic;
using SpaceInvadersMVP.Util.Enum;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Installer
{
    [CreateAssetMenu(fileName = "CombatUIInstaller", menuName = "Installers/CombatUIInstaller")]
    public class CombatUIInstaller : UIInstaller<CombatState>
    {
    }
}
