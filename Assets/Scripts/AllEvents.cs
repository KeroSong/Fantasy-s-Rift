using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SDD.Events;


#region GameManager Events
public class GameMenuEvent : SDD.Events.Event
{
}
public class GameLoadEvent : SDD.Events.Event
{
}
public class GameNewPartyEvent : SDD.Events.Event
{
}
public class GameSelectPlayerEvent : SDD.Events.Event
{
}
public class GamePlayEvent : SDD.Events.Event
{
}
public class GameFightEvent : SDD.Events.Event
{
}
public class GameSettingsEvent : SDD.Events.Event
{
}
public class GameQuitEvent : SDD.Events.Event
{
}
public class GamePauseEvent : SDD.Events.Event
{
}
public class GameSaveEvent : SDD.Events.Event
{
}
public class GameConfirmedEvent : SDD.Events.Event
{
}
public class GameResumeEvent : SDD.Events.Event
{
}
public class GameOverEvent : SDD.Events.Event
{
}
public class GameVictoryFightEvent : SDD.Events.Event
{
}
public class GameInventoryEvent : SDD.Events.Event
{
}
public class GameEquipmentEvent : SDD.Events.Event
{
}
public class GameShopEvent : SDD.Events.Event
{
}
public class GameInnEvent : SDD.Events.Event
{
}
/*public class GameStatisticsChangedEvent : SDD.Events.Event
{
	//public int eBestScore { get; set; }
	public int eScore { get; set; }
	public float eTimer { get; set; }
	//public int eNLives { get; set; }
	//public int eNEnemiesLeftBeforeVictory { get; set; }
}*/
#endregion

#region MenuManager Events
public class ContinuePartyButtonClickedEvent : SDD.Events.Event
{
}
public class NewPartyButtonClickedEvent : SDD.Events.Event
{
}
public class SettingsButtonClickedEvent : SDD.Events.Event
{
}
public class SelectPlayerButtonClickedEvent : SDD.Events.Event
{
}
public class PlayButtonClickedEvent : SDD.Events.Event
{
}
public class FightButtonClickedEvent : SDD.Events.Event
{
}
public class QuitButtonClickedEvent : SDD.Events.Event
{
}
public class SaveButtonClickedEvent : SDD.Events.Event
{
}
public class SavePartyButtonClickedEvent : SDD.Events.Event
{
}
public class LoadButtonClickedEvent : SDD.Events.Event
{
}
public class PauseHasBeenPressEvent : SDD.Events.Event
{
}
public class MainMenuButtonClickedEvent : SDD.Events.Event
{
}
public class ConfirmedButtonClickedEvent : SDD.Events.Event
{
}
public class Button1Event : SDD.Events.Event
{
}
public class Button2Event : SDD.Events.Event
{
}
public class Button3Event : SDD.Events.Event
{
}
/*public class EscapeButtonClickedEvent : SDD.Events.Event
{
}
public class ResumeButtonClickedEvent : SDD.Events.Event
{
}
public class ReplayButtonClickedEvent : SDD.Events.Event
{
}*/
#endregion

#region Collision Events
public class FightCollisionEvent : SDD.Events.Event
{
}
public class FightSoulEaterCollisionEvent : SDD.Events.Event
{
}
public class FightTheNightmareCollisionEvent : SDD.Events.Event
{
}
public class FightTerrorBringerCollisionEvent : SDD.Events.Event
{
}
public class FightUsurperCollisionEvent : SDD.Events.Event
{
}
public class ShopCollisionEvent : SDD.Events.Event
{
}
public class InnCollisionEvent : SDD.Events.Event
{
}
#endregion

/*#region Score Events
public class ScoreHasBeenGainedEvent:SDD.Events.Event
{
	public int eScore;
}
#endregion*/