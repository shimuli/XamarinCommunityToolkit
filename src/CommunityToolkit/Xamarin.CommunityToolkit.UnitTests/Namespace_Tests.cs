﻿using System;
using System.Linq;
using System.Reflection;
using Xamarin.CommunityToolkit.Behaviors.Internals;
using Xamarin.CommunityToolkit.Converters;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xunit;

namespace Xamarin.CommunityToolkit.UnitTests
{
	public class Namespace_Tests
	{
		[Fact]
		public void MakeSureConvertersAreInTheRightNamespace()
		{
			var allTheTypes = Assembly.GetAssembly(typeof(InvertedBoolConverter))?.GetTypes() ?? throw new NullReferenceException();

			foreach (var type in allTheTypes.Where(t => t.Name.EndsWith("Converter") && t.GetInterface(nameof(IValueConverter)) != null))
			{
				Assert.True(type?.Namespace?.Equals("Xamarin.CommunityToolkit.Converters"),
					$"{type?.FullName} not in Xamarin.CommunityToolkit.Converters namespace");
			}
		}

		[Fact]
		public void MakeSureEffectsAreInTheRightNamespace()
		{
			var allTheTypes = Assembly.GetAssembly(typeof(SafeAreaEffect))?.GetTypes() ?? throw new NullReferenceException();

			foreach (var type in allTheTypes.Where(t => t.Name.EndsWith("Effect") && t.IsClass && t.IsSealed && t.IsAbstract))
			{
				Assert.True(type?.Namespace?.Equals("Xamarin.CommunityToolkit.Effects"),
					$"{type?.FullName} not in Xamarin.CommunityToolkit.Effects namespace");
			}
		}

		[Fact]
		public void MakeSureMarkupExtensionsAreInTheRightNamespace()
		{
			var allTheTypes = Assembly.GetAssembly(typeof(TranslateExtension))?.GetTypes() ?? throw new NullReferenceException();

			foreach (var type in allTheTypes.Where(t => t.Name.EndsWith("Extension") && t.GetInterface("IMarkupExtension") != null))
			{
				Assert.True(type?.Namespace?.Equals("Xamarin.CommunityToolkit.Extensions") is true ||
					type?.Namespace?.Equals("Xamarin.CommunityToolkit.Extensions.Internals") is true,
					$"{type?.FullName} not in nameof(Xamarin.CommunityToolkit.Extensions namespace");
			}
		}

		[Fact]
		public void MakeSureBehaviorsAreInTheRightNamespace()
		{
			var allTheTypes = Assembly.GetAssembly(typeof(BaseBehavior<>))?.GetTypes() ?? throw new NullReferenceException();

			foreach (var type in allTheTypes.Where(t => t.Name.EndsWith("Behavior") && t.IsSubclassOf(typeof(BaseBehavior<>))))
			{
				Assert.True(type?.Namespace?.Equals("Xamarin.CommunityToolkit.Behaviors"),
					$"{type?.FullName} not in Xamarin.CommunityToolkit.Behaviors namespace");
			}
		}

		[Fact]
		public void MakeSureViewsAreInTheRightNamespace()
		{
			var allTheTypes = Assembly.GetAssembly(typeof(AvatarView))?.GetTypes() ?? throw new NullReferenceException();

			foreach (var type in allTheTypes.Where(t => t.IsSubclassOf(typeof(View))))
			{
				Assert.True(type?.Namespace?.Equals("Xamarin.CommunityToolkit.UI.Views") is true ||
					type?.Namespace?.Equals("Xamarin.CommunityToolkit.UI.Views.Internals") is true,
					$"{type?.FullName} not in Xamarin.CommunityToolkit.UI.Views namespace");
			}
		}
	}
}