using System;
using System.Linq;
using System.Collections.Generic;
using Beatmap.Base;
using Beatmap.Enums;
using Selector.Actions;
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

        var mapEditorUI = Object.FindObjectOfType<MapEditorUI>();
        _ui.AddMenu(mapEditorUI);
    }

    private static void SelectObject<T>(T obj) where T : BaseObject
    {
        SelectionController.Select(obj, true, false, false);
    }

    private static void DeselectObject<T>(T obj) where T : BaseObject
    {
        SelectionController.Deselect(obj, false);
    }

    public void Select()
    {
        GrabObjects<BaseNote>(ObjectSelectType.Note)
            .Pipe(FilterTime.Perform)
            .Pipe(FilterNoteColor.Perform)
            .Pipe(FilterNoteDirection.Perform)
            .Pipe(FilterGridX.Perform)
            .Pipe(FilterGridY.Perform)
            .ToList().ForEach(SelectObject);
        GrabObjects<BaseNote>(ObjectSelectType.Bomb)
            .Pipe(FilterTime.Perform)
            .Pipe(FilterNoteDirection.Perform)
            .Pipe(FilterGridX.Perform)
            .Pipe(FilterGridY.Perform)
            .ToList().ForEach(SelectObject);
        GrabObjects<BaseNote>(ObjectSelectType.Arc)
            .Pipe(FilterTime.Perform)
            .Pipe(FilterNoteColor.Perform)
            .Pipe(FilterNoteDirection.Perform)
            .Pipe(FilterGridX.Perform)
            .Pipe(FilterGridY.Perform)
            .ToList().ForEach(SelectObject);
        GrabObjects<BaseNote>(ObjectSelectType.Chain)
            .Pipe(FilterTime.Perform)
            .Pipe(FilterNoteColor.Perform)
            .Pipe(FilterNoteDirection.Perform)
            .Pipe(FilterGridX.Perform)
            .Pipe(FilterGridY.Perform)
            .ToList().ForEach(SelectObject);
        GrabObjects<BaseEvent>(ObjectSelectType.Event)
            .Pipe(FilterTime.Perform)
            .Pipe(FilterEventType.Perform)
            .Pipe(FilterEventValue.Perform)
            .Pipe(FilterEventFloatValue.Perform)
            .ToList().ForEach(SelectObject);
        GrabObjects<BaseObstacle>(ObjectSelectType.Obstacle)
            .Pipe(FilterTime.Perform)
            .Pipe(FilterGridX.Perform)
            .Pipe(FilterGridY.Perform)
            .ToList().ForEach(SelectObject);
        
        SelectionController.SelectionChangedEvent?.Invoke();
    }

    public void SelectAll()
    {
        GrabObjects<BaseNote>(ObjectSelectType.Note).ToList().ForEach(SelectObject);
        GrabObjects<BaseNote>(ObjectSelectType.Bomb).ToList().ForEach(SelectObject);
        GrabObjects<BaseNote>(ObjectSelectType.Arc).ToList().ForEach(SelectObject);
        GrabObjects<BaseNote>(ObjectSelectType.Chain).ToList().ForEach(SelectObject);
        GrabObjects<BaseEvent>(ObjectSelectType.Event).ToList().ForEach(SelectObject);
        GrabObjects<BaseObstacle>(ObjectSelectType.Obstacle).ToList().ForEach(SelectObject);
        SelectionController.SelectionChangedEvent?.Invoke();
    }

    public void Deselect()
    {
        GrabObjects<BaseNote>(ObjectSelectType.Note)
            .Pipe(FilterTime.Perform)
            .Pipe(FilterNoteColor.Perform)
            .Pipe(FilterNoteDirection.Perform)
            .Pipe(FilterGridX.Perform)
            .Pipe(FilterGridY.Perform)
            .ToList().ForEach(DeselectObject);
        GrabObjects<BaseNote>(ObjectSelectType.Bomb)
            .Pipe(FilterTime.Perform)
            .Pipe(FilterNoteDirection.Perform)
            .Pipe(FilterGridX.Perform)
            .Pipe(FilterGridY.Perform)
            .ToList().ForEach(DeselectObject);
        GrabObjects<BaseNote>(ObjectSelectType.Arc)
            .Pipe(FilterTime.Perform)
            .Pipe(FilterNoteColor.Perform)
            .Pipe(FilterNoteDirection.Perform)
            .Pipe(FilterGridX.Perform)
            .Pipe(FilterGridY.Perform)
            .ToList().ForEach(DeselectObject);
        GrabObjects<BaseNote>(ObjectSelectType.Chain)
            .Pipe(FilterTime.Perform)
            .Pipe(FilterNoteColor.Perform)
            .Pipe(FilterNoteDirection.Perform)
            .Pipe(FilterGridX.Perform)
            .Pipe(FilterGridY.Perform)
            .ToList().ForEach(DeselectObject);
        GrabObjects<BaseEvent>(ObjectSelectType.Event)
            .Pipe(FilterTime.Perform)
            .Pipe(FilterEventType.Perform)
            .Pipe(FilterEventValue.Perform)
            .Pipe(FilterEventFloatValue.Perform)
            .ToList().ForEach(DeselectObject);
        GrabObjects<BaseObstacle>(ObjectSelectType.Obstacle)
            .Pipe(FilterTime.Perform)
            .Pipe(FilterGridX.Perform)
            .Pipe(FilterGridY.Perform)
            .ToList().ForEach(DeselectObject);

        SelectionController.SelectionChangedEvent?.Invoke();
    }

    public static void DeselectAll()
    {
        SelectionController.DeselectAll();
    }

    private IEnumerable<T> GrabObjects<T>(ObjectSelectType type) where T : BaseObject
    {
        return type switch
        {
            ObjectSelectType.Note => Options.SelectNote
                ? _noteGridContainer.MapObjects.Where(n => n.Type != 3).Cast<T>()
                : [],
            ObjectSelectType.Bomb => Options.SelectBomb
                ? _noteGridContainer.MapObjects.Where(n => n.Type == 3)
                    .Cast<T>()
                : [],
            ObjectSelectType.Arc => Options.SelectArc ? _arcGridContainer.MapObjects.Cast<T>() : [],
            ObjectSelectType.Chain => Options.SelectChain
                ? _chainsGridContainer.MapObjects.Cast<T>()
                : [],
            ObjectSelectType.Event => Options.SelectEvent
                ? _eventGridContainer.MapObjects.Cast<T>()
                : [],
            ObjectSelectType.Obstacle => Options.SelectObstacle
                ? _obstacleGridContainer.MapObjects.Cast<T>()
                : []
        };
    }
}