namespace LibraryManagementSystem.Common.Resources;

public enum Collection
{
    Fiction,
    NonFiction,
    Biography,
    Mystery,
    ScienceFiction,
    UrbanFiction,
    Romance,
}

public enum Format
{
    Hardcover,
    Paperback,
    Audiobook,
    LargeType,
    eBook,
}

public enum CircStatus
{
    In,
    Out,
    Held,
    Lost,
    Withdrawn,
    InTransit,
}