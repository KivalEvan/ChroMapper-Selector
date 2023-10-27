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
        private BPMChangeGridContainer _bpmGridContainer;
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
                _bpmGridContainer = Object.FindObjectOfType<BPMChangeGridContainer>();

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
            var bombs = new List<BaseBombNote>();
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

        private float _adjustTimeStart = 0;
        private float _adjustTimeEnd = 0;
        private bool ObjectFilterTime(BaseObject obj)
        {
            return obj.JsonTime >= _adjustTimeStart && obj.JsonTime <= _adjustTimeEnd;
        }

        private void FilterTime(ref List<BaseNote> notes, ref List<BaseBombNote> bombs, ref List<BaseArc> arcs,
            ref List<BaseChain> chains, ref List<BaseEvent> events, ref List<BaseObstacle> obstacles)
        {
            if (!Options.TimeSelect) return;
            _adjustTimeStart = Options.TimeStart - Options.TimeTolerance;
            _adjustTimeEnd = Options.TimeEnd + Options.TimeTolerance;
            
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
            var color = -1;
            switch (Options.GridColor)
            {
                case "Red":
                    color = 0;
                    break;
                case "Blue":
                    color = 1;
                    break;
            }
            
            notes = new List<BaseNote>(notes.Where(obj => obj.Color == color));
            arcs = new List<BaseArc>(arcs.Where(obj => obj.Color == color));
            chains = new List<BaseChain>(chains.Where(obj => obj.Color == color));
        }

        private void FilterDirection(ref List<BaseNote> notes, ref List<BaseArc> arcs,
            ref List<BaseChain> chains)
        {
            if (!Options.GridDirectionSelect) return;
            var direction = -1;
            switch (Options.GridDirection)
            {
                case "Up":
                    direction = 0;
                    break;
                case "Down":
                    direction = 1;
                    break;
                case "Left":
                    direction = 2;
                    break;
                case "Right":
                    direction = 3;
                    break;
                case "Up-Left":
                    direction = 4;
                    break;
                case "Up-Right":
                    direction = 5;
                    break;
                case "Down-Left":
                    direction = 6;
                    break;
                case "Down-Right":
                    direction = 7;
                    break;
                case "Any":
                    direction = 8;
                    break;
                case "Unknown":
                    direction = 9;
                    break;
                case "ME":
                    direction = 10;
                    break;
            }

            switch (direction)
            {
                case 9:
                    notes = new List<BaseNote>(notes.Where(obj => obj.CutDirection < 0 || obj.CutDirection > 8));
                    arcs = new List<BaseArc>(arcs.Where(obj => obj.CutDirection < 0 || obj.CutDirection > 8));
                    chains = new List<BaseChain>(chains.Where(obj => obj.CutDirection < 0 || obj.CutDirection > 8));
                    break;
                case 10:
                    notes = new List<BaseNote>(notes.Where(obj => obj.CutDirection >= 1000 && obj.CutDirection <= 1360));
                    arcs = new List<BaseArc>(arcs.Where(obj => (obj.CutDirection >= 1000 && obj.CutDirection <= 1360) || (obj.CutDirection >= 2000 && obj.CutDirection <= 2360)));
                    chains = new List<BaseChain>(chains.Where(obj =>
                        (obj.CutDirection >= 1000 && obj.CutDirection <= 1360) || (obj.CutDirection >= 2000 && obj.CutDirection <= 2360)));
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
            var type = -1;
            switch (Options.EventType)
            {
                case "0 - Back top":
                    type = 0;
                    break;    
                case "1 - Ring":
                    type = 1;
                    break;    
                case "2 - L Laser":
                    type = 2;
                    break;    
                case "3 - R Laser":
                    type = 3;
                    break;    
                case "4 - Center":
                    type = 4;
                    break;    
                case "5 - Boost":
                    type = 5;
                    break;    
                case "6 - XL Light":
                    type = 6;
                    break;    
                case "7 - XR Light":
                    type = 7;
                    break;    
                case "8 - Ring Rot":
                    type = 8;
                    break;    
                case "9 - Ring Zoom":
                    type = 9;
                    break;    
                case "10 - XL Laser":
                    type = 10;
                    break;    
                case "11 - XR Laser":
                    type = 11;
                    break;    
                case "12 - L Rot":
                    type = 12;
                    break;    
                case "13 - R Rot":
                    type = 13;
                    break;    
                case "14 - Early Rot":
                    type = 14;
                    break;    
                case "15 - Late Rot":
                    type = 15;
                    break;    
                case "16 - Utility 0":
                    type = 16;
                    break;    
                case "17 - Utility 1":
                    type = 17;
                    break;    
                case "18 - Utility 2":
                    type = 18;
                    break;    
                case "19 - Utility 3":
                    type = 19;
                    break;    
                case "40 - Special 0":
                    type = 40;
                    break;    
                case "41 - Special 1":
                    type = 41;
                    break;    
                case "42 - Special 2":
                    type = 42;
                    break;    
                case "43 - Special 3":
                    type = 43;
                    break;    
                case "100 - BPM":
                    type = 100;
                    break;    
                case "Custom":
                    type = Options.EventTypeCustom;
                    break;    
            }
            
            events = new List<BaseEvent>(events.Where(obj => obj.Type == type));
        }

        private void FilterValue(ref List<BaseEvent> events)
        {
            if (Options.EventValueColorSelect)
            {
                switch (Options.EventValueColor)
                {
                    case "Blue":
                        events = new List<BaseEvent>(events.Where(obj => obj.IsBlue));
                        break;
                    case "Red":
                        events = new List<BaseEvent>(events.Where(obj => obj.IsRed));
                        break;
                    case "White":
                        events = new List<BaseEvent>(events.Where(obj => obj.IsWhite));
                        break;
                    case "Unknown":
                        events = new List<BaseEvent>(events.Where(obj => obj.Value < 0 || obj.Value > 12));
                        break;
                    case "Custom":
                        events = new List<BaseEvent>(events.Where(obj => obj.Value == Options.EventValueCustom));
                        break;
                }
            }
            
            if (Options.EventValueTypeSelect)
            {
                switch (Options.EventValueType)
                {
                    case "Off":
                        events = new List<BaseEvent>(events.Where(obj => obj.IsOff));
                        break;
                    case "On":
                        events = new List<BaseEvent>(events.Where(obj => obj.IsOn));
                        break;
                    case "Flash":
                        events = new List<BaseEvent>(events.Where(obj => obj.IsFlash));
                        break;
                    case "Fade":
                        events = new List<BaseEvent>(events.Where(obj => obj.IsFade));
                        break;
                    case "Transition":
                        events = new List<BaseEvent>(events.Where(obj => obj.IsTransition));
                        break;
                }
            }
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