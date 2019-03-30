using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

namespace Tests
{
    public class BehaviourTreeTest
    {
        [Test]
        public void Sequence_SucessWhenAllChildSucess()
        {
            INode node1 = Substitute.For<INode>();
            INode node2 = Substitute.For<INode>();
            INode node3 = Substitute.For<INode>();

            node1.Tick().Returns(NodeStatus.Sucess);
            node2.Tick().Returns(NodeStatus.Sucess);
            node3.Tick().Returns(NodeStatus.Sucess);

            Sequence sequence = new Sequence(node1, node2, node3);

            NodeStatus status = sequence.Tick();

            while (status == NodeStatus.Running)
            {
                status = sequence.Tick();
            }

            Assert.AreEqual(NodeStatus.Sucess, status);
        }

        [Test]
        public void Sequence_FailureWhenAnyChildFailure()
        {
            INode node1 = Substitute.For<INode>();
            INode node2 = Substitute.For<INode>();
            INode node3 = Substitute.For<INode>();

            node1.Tick().Returns(NodeStatus.Failure);
            node2.Tick().Returns(NodeStatus.Sucess);
            node3.Tick().Returns(NodeStatus.Failure);

            Sequence sequence = new Sequence(node1, node2, node3);

            NodeStatus status = sequence.Tick();

            while (status == NodeStatus.Running)
            {
                status = sequence.Tick();
            }

            Assert.AreEqual(NodeStatus.Failure, status);
        }

        [Test]
        public void Selector_SucessWhenAnyChildSucces()
        {
            INode node1 = Substitute.For<INode>();
            INode node2 = Substitute.For<INode>();
            INode node3 = Substitute.For<INode>();

            node1.Tick().Returns(NodeStatus.Failure);
            node2.Tick().Returns(NodeStatus.Sucess);
            node3.Tick().Returns(NodeStatus.Failure);

            Selector selector = new Selector(node1, node2, node3);

            NodeStatus status = selector.Tick();

            while (status == NodeStatus.Running)
            {
                status = selector.Tick();
            }

            Assert.AreEqual(NodeStatus.Sucess, status);
        }

        [Test]
        public void Succeeder_SucessWhenChildFailure()
        {
            INode node = Substitute.For<INode>();

            node.Tick().Returns(NodeStatus.Failure);

            Succeeder succeeder = new Succeeder(node);

            Assert.AreEqual(NodeStatus.Sucess, succeeder.Tick());
        }
    }
}
