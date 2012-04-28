using System.Collections.Generic;
using System.Runtime.Serialization;
using VX.Domain.DataContracts.Interfaces;

namespace VX.Domain.DataContracts
{
    [DataContract]
    [KnownType(typeof(WordContract))]
    [KnownType(typeof(List<WordContract>))]
    public class TaskContract : ITask
    {
        [DataMember]
        public IWord Question { get; set; }

        [DataMember]
        public IWord CorrectAnswer { get; set; }

        [DataMember]
        public IList<IWord> Answers { get; set; }
    }
}