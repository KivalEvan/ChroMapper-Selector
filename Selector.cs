using System;
using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;
using Selector.UserInterface;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Selector
{
    [Plugin("Selector")]
    public class Selector
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
            _ui = new UI(this);
        }

        private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.buildIndex == 3)
            {
                _noteGridContainer = Object.FindObjectOfType<NoteGridContainer>();
                _eventGridContainer = Object.FindObjectOfType<EventGridContainer>();
                _obstacleGridContainer = Object.FindObjectOfType<ObstacleGridContainer>();
                _arcGridContainer = Object.FindObjectOfType<ArcGridContainer>();
                _chainsGridContainer = Object.FindObjectOfType<ChainGridContainer>();

                var mapEditorUI = Object.FindObjectOfType<MapEditorUI>();
                _ui.AddMenu(mapEditorUI);
            }
        }

        public void Select()
        {
            var notes = new List<BaseNote>();
            var bombs = new List<BaseBombNote>();
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

            notes.ForEach(n => { SelectionController.Select(n, true); });
            bombs.ForEach(n => { SelectionController.Select(n, true); });
            arcs.ForEach(o => { SelectionController.Select(o, true); });
            chains.ForEach(o => { SelectionController.Select(o, true); });
            events.ForEach(e => { SelectionController.Select(e, true); });
            obstacles.ForEach(o => { SelectionController.Select(o, true); });
        }

        public void Deselect()
        {
            var notes = new List<BaseNote>();
            var bombs = new List<BaseBombNote>();
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

            notes.ForEach(n => { SelectionController.Deselect(n); });
            bombs.ForEach(n => { SelectionController.Deselect(n); });
            arcs.ForEach(o => { SelectionController.Deselect(o); });
            chains.ForEach(o => { SelectionController.Deselect(o); });
            events.ForEach(e => { SelectionController.Deselect(e); });
            obstacles.ForEach(o => { SelectionController.Deselect(o); });
        }

        public void SelectAll()
        {
            var notes = new List<BaseNote>();
            var bombs = new List<BaseBombNote>();
            var arcs = new List<BaseArc>();
            var chains = new List<BaseChain>();
            var events = new List<BaseEvent>();
            var obstacles = new List<BaseObstacle>();
            GrabAll(ref notes, ref bombs, ref arcs, ref chains, ref events, ref obstacles);

            notes.ForEach(n => { SelectionController.Select(n, true); });
            bombs.ForEach(n => { SelectionController.Select(n, true); });
            arcs.ForEach(o => { SelectionController.Select(o, true); });
            chains.ForEach(o => { SelectionController.Select(o, true); });
            events.ForEach(e => { SelectionController.Select(e, true); });
            obstacles.ForEach(o => { SelectionController.Select(o, true); });
        }

        public void DeselectAll()
        {
            SelectionController.DeselectAll();
        }

        private void GrabAll(ref List<BaseNote> notes, ref List<BaseBombNote> bombs, ref List<BaseArc> arcs,
            ref List<BaseChain> chains, ref List<BaseEvent> events, ref List<BaseObstacle> obstacles)
        {
            notes = Options.SelectNote
                ? _noteGridContainer.LoadedObjects.Cast<BaseNote>().Where(n => n.Type != 3).ToList()
                : new List<BaseNote>();
            bombs = Options.SelectBomb
                ? _noteGridContainer.LoadedObjects.Cast<BaseNote>().Where(n => n.Type == 3).Cast<BaseBombNote>()
                    .ToList()
                : new List<BaseBombNote>();
            arcs = Options.SelectArc ? _arcGridContainer.LoadedObjects.Cast<BaseArc>().ToList() : new List<BaseArc>();
            chains = Options.SelectChain
                ? _chainsGridContainer.LoadedObjects.Cast<BaseChain>().ToList()
                : new List<BaseChain>();
            events = Options.SelectEvent
                ? _eventGridContainer.LoadedObjects.Cast<BaseEvent>().ToList()
                : new List<BaseEvent>();
            obstacles = Options.SelectObstacle
                ? _obstacleGridContainer.LoadedObjects.Cast<BaseObstacle>().ToList()
                : new List<BaseObstacle>();
        }

        private bool ObjectFilterTime(BaseObject obj)
        {
            return obj.Time >= Options.TimeStart && obj.Time <= Options.TimeEnd;
        }

        private void FilterTime(ref List<BaseNote> notes, ref List<BaseBombNote> bombs, ref List<BaseArc> arcs,
            ref List<BaseChain> chains, ref List<BaseEvent> events, ref List<BaseObstacle> obstacles)
        {
            notes = new List<BaseNote>(notes.Where(ObjectFilterTime));
            bombs = new List<BaseBombNote>(bombs.Where(ObjectFilterTime));
            arcs = new List<BaseArc>(arcs.Where(ObjectFilterTime));
            chains = new List<BaseChain>(chains.Where(ObjectFilterTime));
            events = new List<BaseEvent>(events.Where(ObjectFilterTime));
            obstacles = new List<BaseObstacle>(obstacles.Where(ObjectFilterTime));
        }

        private void FilterColor(ref List<BaseNote> notes, ref List<BaseArc> arcs,
            ref List<BaseChain> chains)
        {
            if (!Options.GridColorSelect) return;
            notes = new List<BaseNote>(notes.Where(obj => obj.Color == Options.GridColor));
            arcs = new List<BaseArc>(arcs.Where(obj => obj.Color == Options.GridColor));
            chains = new List<BaseChain>(chains.Where(obj => obj.Color == Options.GridColor));
        }

        private void FilterDirection(ref List<BaseNote> notes, ref List<BaseArc> arcs,
            ref List<BaseChain> chains)
        {
            if (!Options.GridDirectionSelect) return;
            notes = new List<BaseNote>(notes.Where(obj => obj.CutDirection == Options.GridDirection));
            arcs = new List<BaseArc>(arcs.Where(obj => obj.CutDirection == Options.GridDirection));
            chains = new List<BaseChain>(chains.Where(obj => obj.CutDirection == Options.GridDirection));
        }

        private bool GridFilterX(BaseGrid obj)
        {
            return obj.PosX == Options.GridX;
        }

        private void FilterX(ref List<BaseNote> notes, ref List<BaseBombNote> bombs, ref List<BaseArc> arcs,
            ref List<BaseChain> chains, ref List<BaseObstacle> obstacles)
        {
            if (!Options.GridXSelect) return;
            notes = new List<BaseNote>(notes.Where(GridFilterX));
            bombs = new List<BaseBombNote>(bombs.Where(GridFilterX));
            arcs = new List<BaseArc>(arcs.Where(GridFilterX));
            chains = new List<BaseChain>(chains.Where(GridFilterX));
            obstacles = new List<BaseObstacle>(obstacles.Where(GridFilterX));
        }

        private bool GridFilterY(BaseGrid obj)
        {
            return obj.PosY == Options.GridY;
        }

        private void FilterY(ref List<BaseNote> notes, ref List<BaseBombNote> bombs, ref List<BaseArc> arcs,
            ref List<BaseChain> chains, ref List<BaseObstacle> obstacles)
        {
            if (!Options.GridYSelect) return;
            notes = new List<BaseNote>(notes.Where(GridFilterY));
            bombs = new List<BaseBombNote>(bombs.Where(GridFilterY));
            arcs = new List<BaseArc>(arcs.Where(GridFilterY));
            chains = new List<BaseChain>(chains.Where(GridFilterY));
            obstacles = new List<BaseObstacle>(obstacles.Where(GridFilterY));
        }

        private void FilterType(ref List<BaseEvent> events)
        {
            if (!Options.EventTypeSelect) return;
            events = new List<BaseEvent>(events.Where(obj => obj.Type == Options.EventType));
        }

        private void FilterValue(ref List<BaseEvent> events)
        {
            if (!Options.EventValueSelect) return;
            events = new List<BaseEvent>(events.Where(obj => obj.Value == Options.EventValue));
        }

        private void FilterFloatValue(ref List<BaseEvent> events)
        {
            if (!Options.EventFloatValueSelect) return;
            events = new List<BaseEvent>(events.Where(obj =>
                Math.Abs(obj.FloatValue - Options.EventFloatValue) <= Options.EventFloatValueTolerance));
        }

        [Exit]
        private void Exit()
        {
        }
    }
}