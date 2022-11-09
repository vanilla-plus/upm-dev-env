using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.Geocodes
{
    public interface IGeocode<G>
        where G : IGeocode<G>
    {

        double Latitude
        {
            get;
            set;
        }
        
        double Longitude
        {
            get;
            set;
        }

        void OnValidate()
        {
            
        }
        
        string Geocode
        {
            get;
            set;
        }


//        void UpdateNeighbours(G input,
//                              ref G[] results);


        void UpdateNeighbours(G northWest,
                              G north,
                              G northEast,
                              G west,
                              G east,
                              G southWest,
                              G south,
                              G southEast);


        // This would be nice but it would also need to be static and that can't happen from an interface. Bummer.

//        string CoordinateToCode(double latitude,
//                                double longitude);
//
//
//        (double, double) CodeToCoordinate(string code);

    }
}
