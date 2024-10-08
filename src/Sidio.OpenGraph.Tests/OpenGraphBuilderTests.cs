﻿using Microsoft.Extensions.Logging.Abstractions;

namespace Sidio.OpenGraph.Tests;

public sealed class OpenGraphBuilderTests
{
    private readonly Fixture _fixture = new ();

    [Fact]
    public void Add_ByTag_ShouldBeAdded()
    {
        // arrange
        var builder = new OpenGraphBuilder(NullLogger<OpenGraphBuilder>.Instance);
        var meta = new OpenGraphMetaTag("property1", "content1");

        // act
        var result = builder.Add(meta);

        // assert
        result.Should().BeTrue();
        builder.MetaTags.Count.Should().Be(1);
        builder.MetaTags.First().Should().Be(meta);
    }

    [Fact]
    public void Add_ByParameters_ShouldBeAdded()
    {
        // arrange
        var builder = new OpenGraphBuilder(NullLogger<OpenGraphBuilder>.Instance);
        var propertyName = _fixture.Create<string>();
        var content = _fixture.Create<string>();

        // act
        var result = builder.Add(propertyName, content);

        // assert
        result.Should().BeTrue();
        builder.MetaTags.Count.Should().Be(1);
        builder.MetaTags.First().PropertyName.Should().Be(propertyName);
        builder.MetaTags.First().Content.Should().Be(content);
        builder.MetaTags.First().Namespace.Should().BeEquivalentTo(OpenGraphNamespace.OpenGraph);
    }

    [Fact]
    public void Add_ByParametersAndNamespace_ShouldBeAdded()
    {
        // arrange
        var builder = new OpenGraphBuilder(NullLogger<OpenGraphBuilder>.Instance);
        var propertyName = _fixture.Create<string>();
        var content = _fixture.Create<string>();
        var ns = _fixture.Create<OpenGraphNamespace>();

        // act
        var result = builder.Add(propertyName, content, ns);

        // assert
        result.Should().BeTrue();
        builder.MetaTags.Count.Should().Be(1);
        builder.MetaTags.First().PropertyName.Should().Be(propertyName);
        builder.MetaTags.First().Content.Should().Be(content);
        builder.MetaTags.First().Namespace.Should().BeEquivalentTo(ns);
    }

    [Fact]
    public void Add_EqualMeta_ShouldBeAddedOnce()
    {
        // arrange
        var builder = new OpenGraphBuilder(NullLogger<OpenGraphBuilder>.Instance);
        var meta = new OpenGraphMetaTag("property1", "content1");

        // act
        var result1 = builder.Add(meta);
        var result2 = builder.Add(meta);

        // assert
        builder.MetaTags.Count.Should().Be(1);
        result1.Should().BeTrue();
        result2.Should().BeFalse();
    }

    [Fact]
    public void AddRange_ShouldBeAdded()
    {
        // arrange
        var builder = new OpenGraphBuilder(NullLogger<OpenGraphBuilder>.Instance);
        var tags = _fixture.Build<OpenGraphMetaTag>().CreateMany(5).ToList();

        // act
        builder.AddRange(tags);

        // assert
        builder.MetaTags.Count.Should().Be(tags.Count);
        builder.MetaTags.Should().BeEquivalentTo(tags);
    }

    [Fact]
    public void Validate_WithOpenGraphNamespace_ShouldNotThrowException()
    {
        // arrange
        var builder = new OpenGraphBuilder(NullLogger<OpenGraphBuilder>.Instance);
        builder.Add(
            new OpenGraphMetaTag("title", _fixture.Create<string>()));
        builder.Add(
            new OpenGraphMetaTag("type", _fixture.Create<string>()));
        builder.Add(
            new OpenGraphMetaTag("url", _fixture.Create<string>()));
        builder.Add(
            new OpenGraphMetaTag("image", _fixture.Create<string>()));

        // act
        var action = () => builder.Validate();

        // assert
        action.Should().NotThrow();
    }

    [Fact]
    public void Validate_WithOpenGraphNamespaceAndMissingTitle_ShouldThrowException()
    {
        // arrange
        var builder = new OpenGraphBuilder(NullLogger<OpenGraphBuilder>.Instance);
        builder.Add(
            new OpenGraphMetaTag("type", _fixture.Create<string>()));
        builder.Add(
            new OpenGraphMetaTag("url", _fixture.Create<string>()));
        builder.Add(
            new OpenGraphMetaTag("image", _fixture.Create<string>()));

        // act
        var action = () => builder.Validate();

        // assert
        action.Should().ThrowExactly<OpenGraphValidationException>().WithMessage("*title*");
    }

    [Fact]
    public void Namespaces_WithMultipleProperties_ShouldBeOne()
    {
        // arrange
        var builder = new OpenGraphBuilder(NullLogger<OpenGraphBuilder>.Instance);
        builder.Add(
            new OpenGraphMetaTag("title", _fixture.Create<string>()));
        builder.Add(
            new OpenGraphMetaTag("type", _fixture.Create<string>()));

        // act
        var namespaces = builder.Namespaces.ToList();

        // assert
        namespaces.Count.Should().Be(1);
    }

    [Fact]
    public void GetPrefixAttributeValue_WithMultipleProperties_ShouldContainNamespaces()
    {
        // arrange
        var builder = new OpenGraphBuilder(NullLogger<OpenGraphBuilder>.Instance);
        builder.Add(
            new OpenGraphMetaTag("title", _fixture.Create<string>(), new OpenGraphNamespace("ns1", "ns1/ns1")));
        builder.Add(
            new OpenGraphMetaTag("type", _fixture.Create<string>(), new OpenGraphNamespace("ns2", "ns2/ns2")));

        // act
        var prefix = builder.GetPrefixAttributeValue();

        // assert
        prefix.Should().Be("ns1: ns1/ns1 ns2: ns2/ns2");
    }
}