using System;
using System.Linq;
using System.Collections.Generic;
using Beatmap.Base;
using Selector.UserInterface;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Selector;

[Plugin("Selector")]
public class Main
{
    private ArcGridContainer _arcGridContainer;
    private ChainGridContainer _chainsGridContainer;
    private EventGridContainer _eventGridContainer;
    private NoteGridContainer _noteGridContainer;
    private ObstacleGridContainer _obstacleGridContainer;
    private BPMChangeGridContainer _bpmGridContainer;
    private UI _ui;

    [Init]
    private void Init()
    {
        SceneManager.sceneLoaded += SceneLoaded;
        _ui = new(this);
    }

    [Exit]
    private void Exit()
    {
    }

    private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex != 3) return;
        _noteGridContainer = Object.FindObjectOfType<NoteGridContainer>();
        _eventGridContainer = Object.FindObjectOfType<EventGridContainer>();
        _obstacleGridContainer = Object.FindObjectOfType<ObstacleGridContainer>();
        _arcGridContainer = Object.FindObjectOfType<ArcGridContainer>();
        _chainsGridContainer = Object.FindObjectOfType<ChainGridContainer>();
        _bpmGridContainer = Object.FindObjectOfType<BPMChangeGridContainer>();

        var mapEditorUI = Object.FindObjectOfType<MapEditorUI>();
        _ui.AddMenu(mapEditorUI);
    }

    public void Select()
    {
        var notes = new List<BaseNote>();
        var bombs = new List<BaseNote>();
        var arcs = new List<BaseArc>();
        var chains = new List<BaseChain>();
        var events = new List<BaseEvent>();
        var obstacles = new List<BaseObstacle>();
        GrabAll(ref notes, ref bombs, ref arcs, ref chains, ref events, ref obstacles);

        FilterTime(ref notes, ref bombs, ref arcs, ref chains, ref events, ref obstacles);
        FilterColor(ref notes, ref arcs, ref chains);
        FilterDirection(ref notes, ref arcs, ref chains);
        FilterX(ref notes, ref bombs, ref arcs, ref chains, ref obstacles);
        FilterY(ref notes, ref bombs, ref arcs, ref chains, ref obstacles);
        FilterType(ref events);
        FilterValue(ref events);
        FilterFloatValue(ref events);

        notes.ForEach(n => { SelectionController.Select(n, true, false, false); });
        bombs.ForEach(n => { SelectionController.Select(n, true, false, false); });
        arcs.ForEach(o => { SelectionController.Select(o, true, false, false); });
        chains.ForEach(o => { SelectionController.Select(o, true, false, false); });
        events.ForEach(e => { SelectionController.Select(e, true, false, false); });
        obstacles.ForEach(o => { SelectionController.Select(o, true, false, false); });
        SelectionController.SelectionChangedEvent?.Invoke();
    }

    public void Deselect()
    {
        var notes = new List<BaseNote>();
        var bombs = new List<BaseNote>();
        var arcs = new List<BaseArc>();
        var chains = new List<BaseChain>();
        var events = new List<BaseEvent>();
        var obstacles = new List<BaseObstacle>();
        GrabAll(ref notes, ref bombs, ref arcs, ref chains, ref events, ref obstacles);

        FilterTime(ref notes, ref bombs, ref arcs, ref chains, ref events, ref obstacles);
        FilterColor(ref notes, ref arcs, ref chains);
        FilterDirection(ref notes, ref arcs, ref chains);
        FilterX(ref notes, ref bombs, ref arcs, ref chains, ref obstacles);
        FilterY(ref notes, ref bombs, ref arcs, ref chains, ref obstacles);
        FilterType(ref events);
        FilterValue(ref events);
        FilterFloatValue(ref events);

        notes.ForEach(n => { SelectionController.Deselect(n, false); });
        bombs.ForEach(n => { SelectionController.Deselect(n, false); });
        arcs.ForEach(o => { SelectionController.Deselect(o, false); });
        chains.ForEach(o => { SelectionController.Deselect(o, false); });
        events.ForEach(e => { SelectionController.Deselect(e, false); });
        obstacles.ForEach(o => { SelectionController.Deselect(o, false); });
        SelectionController.SelectionChangedEvent?.Invoke();
    }

    public void SelectAll()
    {
        var notes = new List<BaseNote>();
        var bombs = new List<BaseNote>();
        var arcs = new List<BaseArc>();
        var chains = new List<BaseChain>();
        var events = new List<BaseEvent>();
        var obstacles = new List<BaseObstacle>();
        GrabAll(ref notes, ref bombs, ref arcs, ref chains, ref events, ref obstacles);

        notes.ForEach(n => { SelectionController.Select(n, true, false, false); });
        bombs.ForEach(n => { SelectionController.Select(n, true, false, false); });
        arcs.ForEach(o => { SelectionController.Select(o, true, false, false); });
        chains.ForEach(o => { SelectionController.Select(o, true, false, false); });
        events.ForEach(e => { SelectionController.Select(e, true, false, false); });
        obstacles.ForEach(o => { SelectionController.Select(o, true, false, false); });
        SelectionController.SelectionChangedEvent?.Invoke();
    }

    public void DeselectAll()
    {
        SelectionController.DeselectAll();
    }

    private void GrabAll(ref List<BaseNote> notes, ref List<BaseNote> bombs, ref List<BaseArc> arcs,
        ref List<BaseChain> chains, ref List<BaseEvent> events, ref List<BaseObstacle> obstacles)
    {
        notes = Options.SelectNote
            ? _noteGridContainer.MapObjects.Where(n => n.Type != 3).ToList()
            : notes;
        bombs = Options.SelectBomb
            ? _noteGridContainer.MapObjects.Where(n => n.Type == 3).Cast<BaseNote>()
                .ToList()
            : bombs;
        arcs = Options.SelectArc ? _arcGridContainer.MapObjects.ToList() : arcs;
        chains = Options.SelectChain
            ? _chainsGridContainer.MapObjects.ToList()
            : chains;
        events = Options.SelectEvent
            ? _eventGridContainer.MapObjects.ToList()
            : events;
        obstacles = Options.SelectObstacle
            ? _obstacleGridContainer.MapObjects.ToList()
            : obstacles;
    }

    private float _adjustTimeStart;
    private float _adjustTimeEnd;

    private bool ObjectFilterTime(BaseObject obj)
    {
        return obj.JsonTime >= _adjustTimeStart && obj.JsonTime <= _adjustTimeEnd;
    }

    private void FilterTime(ref List<BaseNote> notes, ref List<BaseNote> bombs, ref List<BaseArc> arcs,
        ref List<BaseChain> chains, ref List<BaseEvent> events, ref List<BaseObstacle> obstacles)
    {
        if (!Options.TimeSelect) return;
        _adjustTimeStart = Options.TimeOperand1 - Options.TimeTolerance;
        _adjustTimeEnd = Options.TimeOperand2 + Options.TimeTolerance;

        notes = new List<BaseNote>(notes.Where(ObjectFilterTime));
        bombs = new List<BaseNote>(bombs.Where(ObjectFilterTime));
        arcs = new List<BaseArc>(arcs.Where(ObjectFilterTime));
        chains = new List<BaseChain>(chains.Where(ObjectFilterTime));
        events = new List<BaseEvent>(events.Where(ObjectFilterTime));
        obstacles = new List<BaseObstacle>(obstacles.Where(ObjectFilterTime));
    }

    private void FilterColor(ref List<BaseNote> notes, ref List<BaseArc> arcs,
        ref List<BaseChain> chains)
    {
        if (!Options.GridColorSelect) return;
        var color = Options.GridColor.id;

        notes = new List<BaseNote>(notes.Where(obj => obj.Color == color));
        arcs = new List<BaseArc>(arcs.Where(obj => obj.Color == color));
        chains = new List<BaseChain>(chains.Where(obj => obj.Color == color));
    }

    private void FilterDirection(ref List<BaseNote> notes, ref List<BaseArc> arcs,
        ref List<BaseChain> chains)
    {
        if (!Options.GridDirectionSelect) return;
        var direction = Options.GridDirection.id;

        switch (Options.GridDirection.name)
        {
            case "Unknown":
                notes = new List<BaseNote>(notes.Where(obj => obj.CutDirection < 0 || obj.CutDirection > 8));
                arcs = new List<BaseArc>(arcs.Where(obj => obj.CutDirection < 0 || obj.CutDirection > 8));
                chains = new List<BaseChain>(chains.Where(obj => obj.CutDirection < 0 || obj.CutDirection > 8));
                break;
            case "ME":
                notes = new List<BaseNote>(notes.Where(obj => obj.CutDirection >= 1000 && obj.CutDirection <= 1360));
                arcs = new List<BaseArc>(arcs.Where(obj =>
                    (obj.CutDirection >= 1000 && obj.CutDirection <= 1360) ||
                    (obj.CutDirection >= 2000 && obj.CutDirection <= 2360)));
                chains = new List<BaseChain>(chains.Where(obj =>
                    (obj.CutDirection >= 1000 && obj.CutDirection <= 1360) ||
                    (obj.CutDirection >= 2000 && obj.CutDirection <= 2360)));
                break;
            default:
                notes = new List<BaseNote>(notes.Where(obj => obj.CutDirection == direction));
                arcs = new List<BaseArc>(arcs.Where(obj => obj.CutDirection == direction));
                chains = new List<BaseChain>(chains.Where(obj => obj.CutDirection == direction));
                break;
        }
    }

    private bool GridFilterX(BaseGrid obj)
    {
        return obj.PosX == Options.GridX;
    }

    private void FilterX(ref List<BaseNote> notes, ref List<BaseNote> bombs, ref List<BaseArc> arcs,
        ref List<BaseChain> chains, ref List<BaseObstacle> obstacles)
    {
        if (!Options.GridXSelect) return;
        notes = new List<BaseNote>(notes.Where(GridFilterX));
        bombs = new List<BaseNote>(bombs.Where(GridFilterX));
        arcs = new List<BaseArc>(arcs.Where(GridFilterX));
        chains = new List<BaseChain>(chains.Where(GridFilterX));
        obstacles = new List<BaseObstacle>(obstacles.Where(GridFilterX));
    }

    private bool GridFilterY(BaseGrid obj)
    {
        return obj.PosY == Options.GridY;
    }

    private void FilterY(ref List<BaseNote> notes, ref List<BaseNote> bombs, ref List<BaseArc> arcs,
        ref List<BaseChain> chains, ref List<BaseObstacle> obstacles)
    {
        if (!Options.GridYSelect) return;
        notes = new List<BaseNote>(notes.Where(GridFilterY));
        bombs = new List<BaseNote>(bombs.Where(GridFilterY));
        arcs = new List<BaseArc>(arcs.Where(GridFilterY));
        chains = new List<BaseChain>(chains.Where(GridFilterY));
        obstacles = new List<BaseObstacle>(obstacles.Where(GridFilterY));
    }

    private void FilterType(ref List<BaseEvent> events)
    {
        if (!Options.EventTypeSelect) return;
        var type = Options.EventType.name switch
        {
            "Custom" => Options.EventTypeCustom,
            _ => Options.EventType.id
        };

        events = new List<BaseEvent>(events.Where(obj => obj.Type == type));
    }

    private void FilterValue(ref List<BaseEvent> events)
    {
        if (Options.EventValueColorSelect)
        {
            events = Options.EventValueColor.name switch
            {
                "Blue" => new List<BaseEvent>(events.Where(obj => obj.IsBlue)),
                "Red" => new List<BaseEvent>(events.Where(obj => obj.IsRed)),
                "White" => new List<BaseEvent>(events.Where(obj => obj.IsWhite)),
                "Unknown" => new List<BaseEvent>(events.Where(obj => obj.Value < 0 || obj.Value > 12)),
                "Custom" => new List<BaseEvent>(events.Where(obj => obj.Value == Options.EventValueCustom)),
                _ => events
            };
        }

        if (Options.EventValueTypeSelect)
        {
            events = Options.EventValueType.name switch
            {
                "Off" => new List<BaseEvent>(events.Where(obj => obj.IsOff)),
                "On" => new List<BaseEvent>(events.Where(obj => obj.IsOn)),
                "Flash" => new List<BaseEvent>(events.Where(obj => obj.IsFlash)),
                "Fade" => new List<BaseEvent>(events.Where(obj => obj.IsFade)),
                "Transition" => new List<BaseEvent>(events.Where(obj => obj.IsTransition)),
                _ => events
            };
        }
    }

    private void FilterFloatValue(ref List<BaseEvent> events)
    {
        if (!Options.EventFloatValueSelect) return;
        events = new List<BaseEvent>(events.Where(obj =>
            Math.Abs(obj.FloatValue - Options.EventFloatValue) <= Options.EventFloatValueTolerance));
    }
}