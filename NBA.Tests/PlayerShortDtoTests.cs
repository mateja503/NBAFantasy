using System.Text.Json;
using NBA.Data.Enumerations;
using NBA.Data.Redis.Dtos;
using NBA.Data.Redis.Entities;
using Xunit;

namespace NBA.Tests
{
    // PlayerShortDto is what DraftState carries, so it is both the /draftHub payload and the JSON stored
    // in draft:state. These pin the one thing that distinguishes it from PlayerShort: the position is a
    // label, never the PlayerPositionEnum code.
    public class PlayerShortDtoTests
    {
        [Theory]
        [InlineData((int)PlayerPositionEnum.G, "G")]
        [InlineData((int)PlayerPositionEnum.F, "F")]
        [InlineData((int)PlayerPositionEnum.C, "C")]
        [InlineData((int)PlayerPositionEnum.GF, "GF")]
        [InlineData((int)PlayerPositionEnum.CF, "CF")]
        [InlineData((int)PlayerPositionEnum.FG, "FG")]
        [InlineData((int)PlayerPositionEnum.UNKOWN, "UNKOWN")]
        [InlineData(null, "UNKOWN")]
        public void ToPlayerShortDto_maps_every_code_to_the_label_the_player_dtos_use(int? code, string expected)
        {
            var dto = new PlayerShort { Position = code }.ToPlayerShortDto();

            Assert.Equal(expected, dto.Position);
        }

        [Fact]
        public void ToPlayerShortDto_carries_the_identity_fields_across()
        {
            var dto = new PlayerShort
            {
                PlayerId = 21,
                FullName = "Nikola Jokic",
                Position = (int)PlayerPositionEnum.C,
            }.ToPlayerShortDto();

            Assert.Equal(21, dto.PlayerId);
            Assert.Equal("Nikola Jokic", dto.FullName);
            Assert.Equal("C", dto.Position);
        }

        [Fact]
        public void ToPlayerShortDtos_maps_a_whole_board_and_keeps_its_order()
        {
            var board = new List<PlayerShort>
            {
                new() { PlayerId = 1, Position = (int)PlayerPositionEnum.G },
                new() { PlayerId = 2, Position = (int)PlayerPositionEnum.CF },
            };

            var dtos = board.ToPlayerShortDtos();

            Assert.Equal([1L, 2L], dtos.Select(p => p.PlayerId));
            Assert.Equal(["G", "CF"], dtos.Select(p => p.Position));
        }

        // What lands in draft:state and in a /draftHub frame: a string, with no numeric code anywhere.
        [Fact]
        public void The_serialized_dto_carries_a_label_and_no_code()
        {
            var json = JsonSerializer.Serialize(
                new PlayerShort { PlayerId = 21, FullName = "Nikola Jokic", Position = (int)PlayerPositionEnum.C }
                    .ToPlayerShortDto());

            Assert.Contains("\"Position\":\"C\"", json);
            Assert.DoesNotContain("\"Position\":3", json);
        }
    }
}
