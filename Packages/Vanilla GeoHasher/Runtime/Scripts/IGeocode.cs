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
        
        string Hash
        {
            get;
            set;
        }

        string Encode(double latitude, double longitude);

        (double, double) Decode(string hash);

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
