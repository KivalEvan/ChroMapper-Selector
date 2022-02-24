using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using Selector.UserInterface;
using Options = Selector.Options;

namespace Selector
{
    [Plugin("Selector")]
    public class Selector
    {
        private UI _ui;
        private NotesContainer _notesContainer;
        private EventsContainer _eventsContainer;
        private ObstaclesContainer _obstaclesContainer;

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
                _notesContainer = UnityEngine.Object.FindObjectOfType<NotesContainer>();
                _eventsContainer = UnityEngine.Object.FindObjectOfType<EventsContainer>();
                _obstaclesContainer = UnityEngine.Object.FindObjectOfType<ObstaclesContainer>();

                MapEditorUI mapEditorUI = UnityEngine.Object.FindObjectOfType<MapEditorUI>();
                _ui.AddMenu(mapEditorUI);
            }
        }

        public void Select()
        {
            List<BeatmapNote> notes = Options.SelectNote ? _notesContainer.LoadedObjects.Cast<BeatmapNote>().ToList() : new List<BeatmapNote>();
            List<MapEvent> events = Options.SelectEvent ? _eventsContainer.LoadedObjects.Cast<MapEvent>().ToList() : new List<MapEvent>();
            List<BeatmapObstacle> obstacles = Options.SelectObstacle ? _obstaclesContainer.LoadedObjects.Cast<BeatmapObstacle>().ToList() : new List<BeatmapObstacle>();
            notes = new List<BeatmapNote>(notes.Where(x => x.Time >= Options.TimeStart && x.Time <= Options.TimeEnd));
            events = new List<MapEvent>(events.Where(x => x.Time >= Options.TimeStart && x.Time <= Options.TimeEnd));
            obstacles = new List<BeatmapObstacle>(obstacles.Where(x => x.Time >= Options.TimeStart && x.Time <= Options.TimeEnd));
            if (Options.StrictType)
            {
                notes = new List<BeatmapNote>(notes.Where(x => x.Type == Options.Type));
                events = new List<MapEvent>(events.Where(x => x.Type == Options.Type));
                obstacles = new List<BeatmapObstacle>(obstacles.Where(x => x.Type == Options.Type));
            }
            if (Options.StrictValue)
            {
                events = new List<MapEvent>(events.Where(x => x.Value == Options.Value));
            }
            notes.ForEach(n => { SelectionController.Select(n, true); });
            events.ForEach(e => { SelectionController.Select(e, true); });
            obstacles.ForEach(o => { SelectionController.Select(o, true); });
        }

        public void Deselect()
        {
            List<BeatmapNote> notes = Options.SelectNote ? _notesContainer.LoadedObjects.Cast<BeatmapNote>().ToList() : new List<BeatmapNote>();
            List<MapEvent> events = Options.SelectEvent ? _eventsContainer.LoadedObjects.Cast<MapEvent>().ToList() : new List<MapEvent>();
            List<BeatmapObstacle> obstacles = Options.SelectObstacle ? _obstaclesContainer.LoadedObjects.Cast<BeatmapObstacle>().ToList() : new List<BeatmapObstacle>();
            notes = new List<BeatmapNote>(notes.Where(x => x.Time >= Options.TimeStart && x.Time <= Options.TimeEnd));
            events = new List<MapEvent>(events.Where(x => x.Time >= Options.TimeStart && x.Time <= Options.TimeEnd));
            obstacles = new List<BeatmapObstacle>(obstacles.Where(x => x.Time >= Options.TimeStart && x.Time <= Options.TimeEnd));
            if (Options.StrictType)
            {
                notes = new List<BeatmapNote>(notes.Where(x => x.Type == Options.Type));
                events = new List<MapEvent>(events.Where(x => x.Type == Options.Type));
                obstacles = new List<BeatmapObstacle>(obstacles.Where(x => x.Type == Options.Type));
            }
            if (Options.StrictValue)
            {
                events = new List<MapEvent>(events.Where(x => x.Value == Options.Value));
            }
            notes.ForEach(n => { SelectionController.Deselect(n); });
            events.ForEach(e => { SelectionController.Deselect(e); });
            obstacles.ForEach(o => { SelectionController.Deselect(o); });
        }

        public void SelectAll()
        {
            List<BeatmapNote> notes = Options.SelectNote ? _notesContainer.LoadedObjects.Cast<BeatmapNote>().ToList() : new List<BeatmapNote>();
            List<MapEvent> events = Options.SelectEvent ? _eventsContainer.LoadedObjects.Cast<MapEvent>().ToList() : new List<MapEvent>();
            List<BeatmapObstacle> obstacles = Options.SelectObstacle ? _obstaclesContainer.LoadedObjects.Cast<BeatmapObstacle>().ToList() : new List<BeatmapObstacle>();
            notes.ForEach(n => { SelectionController.Select(n, true); });
            events.ForEach(e => { SelectionController.Select(e, true); });
            obstacles.ForEach(o => { SelectionController.Select(o, true); });
        }

        public void DeselectAll()
        {
            SelectionController.DeselectAll();
        }

        [Exit]
        private void Exit() { }
    }
}