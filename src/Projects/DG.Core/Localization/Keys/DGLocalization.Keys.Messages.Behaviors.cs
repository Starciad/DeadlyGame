namespace DeadlyGame.Core.Localization
{
    // ================================ //
    //
    // MESSAGES - BEHAVIOURS (KEYS)
    //
    // ================================ //
    public static partial class DGLocalization
    {
        // ========================== //

        #region PROPERTIES
        // Aggressive section
        public static string MESSAGES_BEHAVIOR_AGGRESSIVE_NAME => lMessagesBehaviors.GetSection("aggressive").GetKey("name");
        public static string MESSAGES_BEHAVIOR_AGGRESSIVE_IS_CRITICAL => lMessagesBehaviors.GetSection("aggressive").GetKey("is_critical");

        // Crafting section
        public static string MESSAGES_BEHAVIOR_CRAFTING_NAME => lMessagesBehaviors.GetSection("crafting").GetKey("name");

        // Item Acquisition section
        public static string MESSAGES_BEHAVIOR_ITEM_ACQUISITION_NAME => lMessagesBehaviors.GetSection("item_acquisition").GetKey("name");

        // Movement section
        public static string MESSAGES_BEHAVIOR_MOVEMENT_NAME => lMessagesBehaviors.GetSection("movement").GetKey("name");

        // Resource Acquisition section
        public static string MESSAGES_BEHAVIOR_RESOURCE_ACQUISITION_NAME => lMessagesBehaviors.GetSection("resource_acquisition").GetKey("name");

        // Self Preservation section
        public static string MESSAGES_BEHAVIOR_SELF_PRESERVATION_NAME => lMessagesBehaviors.GetSection("self_preservation").GetKey("name");
        public static string MESSAGES_BEHAVIOR_SELF_PRESERVATION_DESCRIPTION_HEALTH => lMessagesBehaviors.GetSection("self_preservation").GetKey("description_health");
        public static string MESSAGES_BEHAVIOR_SELF_PRESERVATION_DESCRIPTION_HUNGER => lMessagesBehaviors.GetSection("self_preservation").GetKey("description_hunger");
        #endregion

        // ========================== //

        #region METHODS
        // Aggressive section
        public static string GetMessage_Aggressive_Title_Attack(string attacker, string target)
        {
            string sentence = lMessagesBehaviors.GetSection("aggressive").GetKey("title_attack");
            return string.Format(sentence, attacker, target);
        }
        public static string GetMessage_Aggressive_Title_Killed(string killer, string victim)
        {
            string sentence = lMessagesBehaviors.GetSection("aggressive").GetKey("title_killed");
            return string.Format(sentence, killer, victim);
        }
        public static string GetMessage_Aggressive_Description_Attack(string attacker, string damage, string weapon, string opponent)
        {
            string sentence = lMessagesBehaviors.GetSection("aggressive").GetKey("description_attack");
            return string.Format(sentence, attacker, damage, weapon, opponent);
        }
        public static string GetMessage_Aggressive_Description_Killed(string killer, string victim, string damage, string weapon)
        {
            string sentence = lMessagesBehaviors.GetSection("aggressive").GetKey("description_killed");
            return string.Format(sentence, killer, victim, damage, weapon);
        }

        // Crafting section
        public static string GetMessage_Crafting_Title(string crafter)
        {
            string sentence = lMessagesBehaviors.GetSection("crafting").GetKey("title");
            return string.Format(sentence, crafter);
        }
        public static string GetMessage_Crafting_Description(string crafter, string amount, string item)
        {
            string sentence = lMessagesBehaviors.GetSection("crafting").GetKey("description");
            return string.Format(sentence, crafter, amount, item);
        }

        // Item Acquisition section
        public static string GetMessage_ItemAcquisition_Title(string acquirer)
        {
            string sentence = lMessagesBehaviors.GetSection("item_acquisition").GetKey("title");
            return string.Format(sentence, acquirer);
        }
        public static string GetMessage_ItemAcquisition_New_Item(string amount, string item)
        {
            string sentence = lMessagesBehaviors.GetSection("item_acquisition").GetKey("new_item");
            return string.Format(sentence, amount, item);
        }

        // Movement section
        public static string GetMessage_Movement_Title(string mover)
        {
            string sentence = lMessagesBehaviors.GetSection("movement").GetKey("title");
            return string.Format(sentence, mover);
        }
        public static string GetMessage_Movement_Description(string mover, string oldX, string oldY, string newX, string newY)
        {
            string sentence = lMessagesBehaviors.GetSection("movement").GetKey("description");
            return string.Format(sentence, mover, oldX, oldY, newX, newY);
        }

        // Resource Acquisition section
        public static string GetMessage_ResourceAcquisition_Title(string acquirer)
        {
            string sentence = lMessagesBehaviors.GetSection("resource_acquisition").GetKey("title");
            return string.Format(sentence, acquirer);
        }
        public static string GetMessage_ResourceAcquisition_Description(string acquirer, string damage, string resource)
        {
            string sentence = lMessagesBehaviors.GetSection("resource_acquisition").GetKey("description");
            return string.Format(sentence, acquirer, damage, resource);
        }

        // Self Preservation section
        public static string GetMessage_SelfPreservation_Title(string person)
        {
            string sentence = lMessagesBehaviors.GetSection("self_preservation").GetKey("title");
            return string.Format(sentence, person);
        }
        public static string GetMessage_SelfPreservation_Description_Intro(string person)
        {
            string sentence = lMessagesBehaviors.GetSection("self_preservation").GetKey("description_intro");
            return string.Format(sentence, person);
        }

        #endregion

        // ========================== //
    }
}
