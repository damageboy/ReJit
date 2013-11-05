using System;

namespace ReJit {


  public class ILReader {
    [Flags()]
    public enum ILMethodHeader : byte
    {
      CorILMethod_FatFormat = 0x3,
      CorILMethod_TinyFormat = 0x2,
      CorILMethod_MoreSects = 0x8,
      CorILMethod_InitLocals = 0x10
    }

    // codes that identify attributes
    public enum CorILMethodSect : uint
    {
      CorILMethod_Sect_Reserved = 0,
      CorILMethod_Sect_EHTable = 1,
      CorILMethod_Sect_OptILTable = 2,

      // The mask for decoding the type code
      CorILMethod_Sect_KindMask = 0x3F,
      // fat format
      CorILMethod_Sect_FatFormat = 0x40,
      // there is another attribute after this one
      CorILMethod_Sect_MoreSects = 0x80
    }

    // defintitions for the Flags field below (for both big and small)
    public enum CorExceptionFlag : uint
    {
      // This is a typed handler
      COR_ILEXCEPTION_CLAUSE_NONE,
      // Deprecated
      COR_ILEXCEPTION_CLAUSE_OFFSETLEN = 0x0000,
      // Deprecated
      COR_ILEXCEPTION_CLAUSE_DEPRECATED = 0x0000,
      // If this bit is on, then this EH entry is for a filter
      COR_ILEXCEPTION_CLAUSE_FILTER = 0x0001,
      // This clause is a finally clause
      COR_ILEXCEPTION_CLAUSE_FINALLY = 0x0002,
      // Fault clause (finally that is called on exception only)
      COR_ILEXCEPTION_CLAUSE_FAULT = 0x0004,
      // duplicated clase..  this clause was duplicated down to a funclet
      //which was pulled out of line
      COR_ILEXCEPTION_CLAUSE_DUPLICATED = 0x0008
    }

    
   
  }
}