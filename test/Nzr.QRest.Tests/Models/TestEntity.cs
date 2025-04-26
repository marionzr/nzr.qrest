namespace Nzr.QRest.Tests.Models;

public class TestEntity
{
    public short AShort { get; set; }
    public short? ANullableShort { get; set; }
    public int AInt { get; set; }
    public int? ANullableInt { get; set; }
    public long ALong { get; set; }
    public long? ANullableLong { get; set; }
    public double ADouble { get; set; }
    public double? ANullableDouble { get; set; }

    public decimal ADecimal { get; set; }
    public decimal? ANullableDecimal { get; set; }
    public string AString { get; set; }
    public string? ANullableString { get; set; }
    public Status AEnum { get; set; }
    public Status? ANullableEnum { get; set; }
    public bool ABoolean { get; set; }
    public bool? ANullableBoolean { get; set; }
    public DateTimeOffset ADateTimeOffset { get; set; }
    public DateTimeOffset? ANullableDateTimeOffset { get; set; }
    public DateTime ADateTime { get; set; }
    public DateTime? ANullableDateTime { get; set; }

    public TestEntity(
        short aShort, short? aNullableShort,
        int aInt, int? aNullableInt,
        long aLong, long? aNullableLong,
        double aDouble, double? aNullableDouble,
        decimal aDecimal, decimal? aNullableDecimal,
        string aString, string? aNullableString,
        Status aEnum, Status? aNullableEnum,
        bool aBoolean, bool? aNullableBoolean,
        DateTimeOffset aDateTimeOffset, DateTimeOffset? aNullableDateTimeOffset,
        DateTime aDateTime, DateTime? aNullableDateTime)
    {
        AShort = aShort;
        ANullableShort = aNullableShort;
        AInt = aInt;
        ANullableInt = aNullableInt;
        ALong = aLong;
        ANullableLong = aNullableLong;
        ADouble = aDouble;
        ANullableDouble = aNullableDouble;
        ADecimal = aDecimal;
        ANullableDecimal = aNullableDecimal;
        AString = aString;
        ANullableString = aNullableString;
        AEnum = aEnum;
        ANullableEnum = aNullableEnum;
        ABoolean = aBoolean;
        ANullableBoolean = aNullableBoolean;
        ADateTimeOffset = aDateTimeOffset;
        ANullableDateTimeOffset = aNullableDateTimeOffset;
        ADateTime = aDateTime;
        ANullableDateTime = aNullableDateTime;
    }
}
