using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleSolvers.Nanogram {
    internal class UnexpectedEnumValueException<T> : Exception {
        internal UnexpectedEnumValueException(T value)
            : base("Value " + value + " of enum " + typeof(T).Name + " is not supported") {
        }
    }
}
