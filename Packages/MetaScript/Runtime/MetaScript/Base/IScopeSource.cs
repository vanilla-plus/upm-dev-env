using System;

using System.Security.Cryptography;
using System.Text;

using UnityEngine;

namespace Vanilla.MetaScript
{

    /*
    wtf u doin
    The idea of popping scopes in a Dictionary is awesome, as long as you can figure out how to handle their individuality.
    Remember, some tasks will probably just be run once and considered a singleton of sorts, with named cancel support etc
    However, sometimes a task may be invoked potentially hundreds of times and each one would potentially require a unique name in the case of cancellation?
    It all comes down to the idea of inferring a scopes name based on the task/instance that runs it. Instead, we need better modular support for this.
    Most tasks won't have a new scope and can just be nameless - or have a null IScopeSource - but some will require specifically named ones or randomized ones
    It should also be performed in a way that has a positively tiny memory footprint - no fields!
    */
    
    public interface IScopeSource
    {

        public Scope CreateScope(Scope parent);

    }

    // TypeMenu won't list any classes that have a constructor
    // Interesting caveat...? I wonder why?

    [Serializable]
    public class Named_Scope_Source : IScopeSource
    {

        [SerializeField]
        public string name;

        public Scope CreateScope(Scope parent) => new Scope(parent: parent,
                                                            name: name);

    }

    [Serializable]
    public class Indexed_Scope_Source : IScopeSource
    {

        [SerializeField]
        public string prefix;

        public Scope CreateScope(Scope parent)
        {
            var i = 0;

            // Skip through through any pre-existing scopes with this prefix and index until we hit an index that isn't in use
            while (Scope.Get($"{prefix} [{i}]") != null) i++;

            return new Scope(parent: parent,
                             name: $"{prefix} [{i}]");
        }

    }

    [Serializable]
    public class Randomized_Scope_Source : IScopeSource
    {

        public Scope CreateScope(Scope parent) => new Scope(parent: parent,
                                                            name: NewRandomScopeName(16));
        
        private string NewRandomScopeName(sbyte size)
        {
            using var rng = new RNGCryptoServiceProvider();

            var data = new byte[size / 2];

            rng.GetBytes(data);

            var sb = new StringBuilder(size);

            foreach (var b in data) sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

    }

}