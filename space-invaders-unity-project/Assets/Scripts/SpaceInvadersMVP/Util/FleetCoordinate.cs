namespace SpaceInvadersMVP.Util
{
    public struct FleetCoordinate
    {
        public int ColumnIndex;
        public int RowIndex;
        public FleetCoordinate(int columnIndex, int rowIndex)
        {
            ColumnIndex = columnIndex;
            RowIndex = rowIndex;
        }
    }
}
