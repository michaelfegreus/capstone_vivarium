namespace Opsive.UltimateInventorySystem.Equipping
{
    using Opsive.Shared.Game;
    using Opsive.UltimateInventorySystem.Core;
    using Opsive.UltimateInventorySystem.Core.DataStructures;
    using Opsive.UltimateInventorySystem.Core.InventoryCollections;
    using Opsive.UltimateInventorySystem.Storage;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using EventHandler = Opsive.Shared.Events.EventHandler;

    public class EquipperCustom : Equipper
    {
        public Item item;

        public void FieldEquip()
        {
            //Check for available empty slots.
            for (int i = 0; i < m_Slots.Length; i++)
            {
                if (m_Slots[i].ItemObject != null) { continue; }
                if (m_Slots[i].Category != null && m_Slots[i].Category.InherentlyContains(item) == false) { continue; }
            }

            //Check for any slot (even used ones).
            for (int i = 0; i < m_Slots.Length; i++)
            {
                if (m_Slots[i].Category != null && m_Slots[i].Category.InherentlyContains(item) == false) { continue; }
            }
        }
    }
}