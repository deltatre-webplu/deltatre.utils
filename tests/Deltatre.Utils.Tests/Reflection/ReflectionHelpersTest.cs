using Deltatre.Utils.Reflection;
using NUnit.Framework;
using System;

namespace Deltatre.Utils.Tests.Reflection
{
  [TestFixture]
  public class ReflectionHelpersTest
  {
    [Test]
    public void IsDecoratorFor_Throws_ArgumentNullException_When_Type_Is_Null()
    {
      // ACT
      Assert.Throws<ArgumentNullException>(() => ReflectionHelpers.IsDecoratorFor<IService>(null));
    }

    [Test]
    public void IsDecoratorFor_Returns_True_When_Type_Is_Decorator_For_TDecoratee_And_Type_Ctor_Has_One_Parameter()
    {
      // ACT
      var result = ReflectionHelpers.IsDecoratorFor<IService>(typeof(SimpleDecorator));

      // ASSERT
      Assert.IsTrue(result);
    }

    [TestCase(typeof(ComplexDecorator1))]
    [TestCase(typeof(ComplexDecorator2))]
    public void IsDecoratorFor_Returns_True_When_Type_Is_Decorator_For_TDecoratee_And_Type_Ctor_Has_Two_Parameter(Type type)
    {
      // ACT
      var result = ReflectionHelpers.IsDecoratorFor<IService>(type);

      // ASSERT
      Assert.IsTrue(result);
    }

    [Test]
    public void IsDecoratorFor_Returns_True_When_Type_Is_Decorator_For_TDecoratee_And_Is_Sealed_Class()
    {
      // ACT
      var result = ReflectionHelpers.IsDecoratorFor<IService>(typeof(SealedDecorator));

      // ASSERT
      Assert.IsTrue(result);
    }

    [Test]
    public void IsDecoratorFor_Returns_True_When_Type_Is_Decorator_For_TDecoratee_And_Is_Open_Generic_Type()
    {
      // ACT
      var result = ReflectionHelpers.IsDecoratorFor<IService>(typeof(GenericDecorator<>));

      // ASSERT
      Assert.IsTrue(result);
    }

    [Test]
    public void IsDecoratorFor_Returns_True_When_Type_Is_Decorator_For_TDecoratee_And_Is_Closed_Generic_Type()
    {
      // ACT
      var result = ReflectionHelpers.IsDecoratorFor<IService>(typeof(GenericDecorator<string>));

      // ASSERT
      Assert.IsTrue(result);
    }

    [TestCase(typeof(AbstractDecoratorWithPublicCtor))]
    [TestCase(typeof(AbstractDecoratorWithProtectedCtor))]
    public void IsDecoratorFor_Returns_False_When_Type_Is_Decorator_For_TDecoratee_And_Is_Abstract_Class(Type type)
    {
      // ACT
      var result = ReflectionHelpers.IsDecoratorFor<IService>(type);

      // ASSERT
      Assert.IsFalse(result);
    }

    [Test]
    public void IsDecoratorFor_Returns_False_When_Type_Depends_On_TDecoratee_But_Does_Not_Implement_TDecoratee()
    {
      // ACT
      var result = ReflectionHelpers.IsDecoratorFor<IService>(typeof(SomeClass));

      // ASSERT
      Assert.IsFalse(result);
    }

    [Test]
    public void IsDecoratorFor_Returns_False_When_Type_Does_Not_Depend_On_TDecoratee_But_Implements_TDecoratee()
    {
      // ACT
      var result = ReflectionHelpers.IsDecoratorFor<IService>(typeof(SomeOtherClass));

      // ASSERT
      Assert.IsFalse(result);
    }

    [Test]
    public void IsDecoratorFor_Returns_False_When_Type_Implements_Abstraction_And_Has_Non_Public_Ctor_Requiring_Abstraction()
    {
      // ACT
      var result = ReflectionHelpers.IsDecoratorFor<IService>(typeof(ClassWithNoPublicCtor));

      // ASSERT
      Assert.IsFalse(result);
    }

    [Test]
    public void IsDecoratorFor_Returns_False_When_Type_Has_Multiple_Public_Constructors()
    {
      // ACT
      var result = ReflectionHelpers.IsDecoratorFor<IService>(typeof(DecoratorWithTwoPublicCtor));

      // ASSERT
      Assert.IsFalse(result);
    }

    public interface IService
    {

    }

    public class Service : IService
    {
    }

    public class SimpleDecorator : IService
    {
      private readonly IService _decoratee;

      public SimpleDecorator(IService decoratee)
      {
        _decoratee = decoratee;
      }
    }

    public sealed class SealedDecorator : IService
    {
      private readonly IService _decoratee;

      public SealedDecorator(IService decoratee)
      {
        _decoratee = decoratee;
      }
    }

    public class GenericDecorator<T> : IService
    {
      private readonly IService _decoratee;

      public GenericDecorator(IService decoratee)
      {
        _decoratee = decoratee;
      }
    }

    public class ComplexDecorator1 : IService
    {
      private readonly IService _decoratee;
      private readonly string _name;

      public ComplexDecorator1(IService decoratee, string name)
      {
        _decoratee = decoratee;
        _name = name;
      }
    }

    public class ComplexDecorator2 : IService
    {
      private readonly IService _decoratee;
      private readonly string _name;

      public ComplexDecorator2(string name, IService decoratee)
      {
        _decoratee = decoratee;
        _name = name;
      }
    }

    public abstract class AbstractDecoratorWithPublicCtor : IService
    {
      private readonly IService _decoratee;

      public AbstractDecoratorWithPublicCtor(IService decoratee)
      {
        _decoratee = decoratee;
      }
    }

    public abstract class AbstractDecoratorWithProtectedCtor : IService
    {
      private readonly IService _decoratee;

      protected AbstractDecoratorWithProtectedCtor(IService decoratee)
      {
        _decoratee = decoratee;
      }
    }

    public class SomeClass
    {
      private readonly IService _decoratee;

      public SomeClass(IService decoratee)
      {
        _decoratee = decoratee;
      }
    }

    public class SomeOtherClass : IService
    {
    }

    public sealed class ClassWithNoPublicCtor : IService
    {
      private readonly IService _decoratee;

      private ClassWithNoPublicCtor(IService decoratee)
      {
        _decoratee = decoratee;
      }

      public static ClassWithNoPublicCtor Build(IService decoratee)
      {
        return new ClassWithNoPublicCtor(decoratee);
      }
    }

    public class DecoratorWithTwoPublicCtor : IService
    {
      private readonly IService _decoratee;

      public DecoratorWithTwoPublicCtor() 
        :this(new Service())
      {
      }

      public DecoratorWithTwoPublicCtor(IService decoratee)
      {
        _decoratee = decoratee;
      }
    }
  }
}
