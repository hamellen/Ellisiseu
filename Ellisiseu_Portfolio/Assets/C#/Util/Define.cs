using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    {

        Unknown, Login, Lobby, Game
    }
    public enum Monster_State
    {

        IDLE, ATTACK
    }
    public enum Sound
    {

        Bgm, D2_Effect, MaxCount

    }

    

    public enum ItemType
    {

        Equipment, Consumable
    }

    public enum Player_type
    {

        EXP, GOLD, DAMAGE, HEALING
    }

   
    public enum Equipment
    {

        Weapon, Armor,MaxCount
    }
    

    public enum Consumable
    {
        Healing
    }

   
}
