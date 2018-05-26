using System;
using System.Collections;
using System.Data.Common;
using Contract.Core.Errors;
using InvalidOperationException = Contract.Core.Errors.InvalidOperationException;

namespace Contract.Core.Helpers
{
    public class ErrorHelper
    {
        #region :: common

        public static BaseException Failed()
        {
            return new BaseException("Operation failed");
        }

        public static BaseException Failed(string message)
        {
            return new BaseException(message);
        }

        public static BaseException Failed(string source, string message)
        {
            return new BaseException(message) {Source = source};
        }

        public static BaseException Failed(Exception e)
        {
            return new BaseException("Operation failed", e);
        }

        public static BaseException Failed(string source, Exception e)
        {
            return new BaseException("Operation failed", e) {Source = source};
        }

        public static BaseException Failed(string source, string message, Exception e)
        {
            return new BaseException(message, e) {Source = source};
        }

        public static BaseException Failed(string source, string message, Exception e, IDictionary data)
        {
            var be = new BaseException(message, e) {Source = source};
            if (data != null)
                foreach (var item in data.Keys)
                    be.Data.Add(item, data[item]);
            return be;
        }

        public static Exception FormatException()
        {
            return new FormatException();
        }

        public static Exception FormatException(string message)
        {
            return new FormatException(message);
        }

        public static Exception FormatException(string message, Exception inner)
        {
            return new FormatException(message, inner);
        }

        #endregion

        #region :: notallowed

        public static DuplicateEntryException Duplicate(string message)
        {
            return new DuplicateEntryException(message);
        }

        public static NotAllowedException NotAllowed(string message)
        {
            return new NotAllowedException(message);
        }

        public static AuthException AuthFail(string message)
        {
            return new AuthException(message);
        }

        public static AuthException AuthFail(string message, Exception inner)
        {
            return new AuthException(message, inner);
        }

        public static SessionTokenException SessionTokenFail(string message)
        {
            return new SessionTokenException(message);
        }

        public static SessionTokenException SessionTokenFail(string message, Exception inner)
        {
            return new SessionTokenException(message, inner);
        }

        #endregion

        #region :: notfound

        public static NotFoundException NotFound(string message)
        {
            return new NotFoundException(message);
        }

        public static NotFoundException NotFound<T>()
        {
            return new NotFoundException(typeof (T).Name);
        }

        #endregion

        #region :: arguments

        public static ArgumentException Arg(string argName)
        {
            return new ArgumentException("Invalid argument", argName);
        }

        public static ArgumentException Arg(string argName, string message)
        {
            return new ArgumentException(message, argName);
        }

        public static ArgumentNullException ArgNull(string argName)
        {
            return new ArgumentNullException(argName);
        }

        public static ArgumentNullException ArgNull(string argName, string message)
        {
            return new ArgumentNullException(argName, message);
        }

        public static ArgumentOutOfRangeException ArgRange()
        {
            return new ArgumentOutOfRangeException();
        }

        public static ArgumentOutOfRangeException ArgRange(string argName)
        {
            return new ArgumentOutOfRangeException(argName);
        }

        public static ArgumentOutOfRangeException ArgRange(string argName, string message)
        {
            return new ArgumentOutOfRangeException(argName, message);
        }

        public static ArgumentOutOfRangeException ArgRange(string argName, object actualValue, string message)
        {
            return new ArgumentOutOfRangeException(argName, actualValue, message);
        }

        #endregion

        #region :: persistence

        public static PersistenceException Persistence(string message, Exception e)
        {
            return new PersistenceException(message, e);
        }

        public static PersistenceException Persistence(DbException e)
        {
            return new PersistenceException("Query error", e);
        }

        #endregion

        #region :: invalid operation

        public static InvalidOperationException InvalidOperation(string message, Exception e)
        {
            return new InvalidOperationException(message, e);
        }

        public static InvalidOperationException InvalidOperation(string message)
        {
            return new InvalidOperationException(message);
        }

        public static InvalidOperationException InvalidOperation()
        {
            return new InvalidOperationException();
        }

        public static InvalidArgumentException InvalidArgument()
        {
            return new InvalidArgumentException();
        }

        public static InvalidArgumentException InvalidArgument(string message)
        {
            return new InvalidArgumentException(message);
        }

        public static InvalidArgumentException InvalidArgument(string message, Exception ex)
        {
            return new InvalidArgumentException(message, ex);
        }

        public static InvalidImageException InvalidImage()
        {
            return new InvalidImageException();
        }

        public static InvalidImageException InvalidImage(string message)
        {
            return new InvalidImageException(message);
        }

        public static InvalidImageException InvalidImage(string message, Exception ex)
        {
            return new InvalidImageException(message, ex);
        }

        public static InvalidStateException InvalidState()
        {
            return new InvalidStateException();
        }

        public static InvalidStateException InvalidState(string message)
        {
            return new InvalidStateException(message);
        }

        public static InvalidStateException InvalidState(string message, Exception ex)
        {
            return new InvalidStateException(message, ex);
        }

        #endregion

        #region :: Not supported

        public static NotSupportedException NotSupported(string message)
        {
            return new NotSupportedException(message);
        }

        public static NotSupportedException NotSupported()
        {
            return new NotSupportedException();
        }

        #endregion
    }
}