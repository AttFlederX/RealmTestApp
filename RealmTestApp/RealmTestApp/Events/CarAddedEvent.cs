using Prism.Events;
using RealmTestApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealmTestApp.Events {
    public class CarAddedEvent : PubSubEvent<CarDTO> {
    }
}
