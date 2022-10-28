using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;

namespace PetProject.Application.Tests.Csharp;

public class ProgramProperties
{
    [Property]
    public Property DownIncreasesAim() =>
        Prop.ForAll(
            Arb.From<int>(),
            Arb.From<int>(),
            (initial, value) => new Program.Submarine(initial, default, default).ExecuteCommand($"down {value}").Aim
                .Should().Be(value + initial));

    [Property]
    public Property UpDecreasesAim() =>
        Prop.ForAll(
            Arb.From<int>(),
            Arb.From<int>(),
            (initial, value) => new Program.Submarine(initial, default, default).ExecuteCommand($"up {value}").Aim
                .Should().Be(initial - value));

    [Property]
    public Property ForwardIncreasesPosition() =>
        Prop.ForAll(
            Arb.From<int>(),
            Arb.From<int>(),
            (initial, value) => new Program.Submarine(default, initial, default).ExecuteCommand($"forward {value}")
                .Position
                .Should().Be(initial + value));

    [Property]
    public Property ForwardIncreasesDepth() =>
        Prop.ForAll(
            Arb.From<int>(),
            Arb.From<int>(),
            (initialAim, forward) =>
                new Program.Submarine(initialAim, default, default).ExecuteCommand($"forward {forward}").Depth
                    .Should().Be(forward * initialAim));
}