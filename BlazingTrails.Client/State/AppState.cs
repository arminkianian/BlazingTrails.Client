﻿using BlazingTrails.Shared.Features.ManageTrails.Shared;

namespace BlazingTrails.Client.State
{
    public class AppState
    {
        public NewTrailState NewTrailState { get; }
        public AppState()
        {
            NewTrailState = new NewTrailState();
        }
    }
}
