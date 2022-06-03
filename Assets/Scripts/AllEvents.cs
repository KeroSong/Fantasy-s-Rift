using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SDD.Events;


#region GameManager Events
public class GameMenuEvent : SDD.Events.Event
{
}
public class GameNewPartyEvent : SDD.Events.Event
{
}
public class GamePlayEvent : SDD.Events.Event
{
}
public class GameSaveEvent : SDD.Events.Event
{
}
public class GameSettingsEvent : SDD.Events.Event
{
}
public class GamePauseEvent : SDD.Events.Event
{
}
public class GameResumeEvent : SDD.Events.Event
{
}
public class GameOverEvent : SDD.Events.Event
{
}
public class GameVictoryEvent : SDD.Events.Event
{
}
public class GameStatisticsChangedEvent : SDD.Events.Event
{
	//public int eBestScore { get; set; }
	public int eScore { get; set; }
	public float eTimer { get; set; }
	//public int eNLives { get; set; }
	//public int eNEnemiesLeftBeforeVictory { get; set; }
}
#endregion

#region MenuManager Events
public class EscapeButtonClickedEvent : SDD.Events.Event
{
}
public class ContinuePartyClickedEvent : SDD.Events.Event
{
}
public class NewPartyClickedEvent : SDD.Events.Event
{
}
public class SettingsClickedEvent : SDD.Events.Event
{
}
public class PlayButtonClickedEvent : SDD.Events.Event
{
}
public class QuitClickedEvent : SDD.Events.Event
{
}
public class ResumeButtonClickedEvent : SDD.Events.Event
{
}
public class ReplayButtonClickedEvent : SDD.Events.Event
{
}
public class MainMenuButtonClickedEvent : SDD.Events.Event
{
}
#endregion

#region Score events
public class ScoreHasBeenGainedEvent:SDD.Events.Event
{
	public int eScore;
}
#endregion