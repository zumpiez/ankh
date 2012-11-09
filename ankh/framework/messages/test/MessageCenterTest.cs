﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ankh.framework.messages.test
{
    [TestFixture]
    class MessageCenterTest
    {
        [Test]
        public void TopicCanBePublished()
        {
            var listener = new ListenerThing();
            
            var topic = new SomeTopic();
            topic.data = 500;
            MessageCenter<SomeTopic>.Publish(topic);

            Assert.AreEqual(listener.data, topic.data);
        }

        [Test]
        public void ListenersDieGracefully()
        {
            ListenerThing listener = new ListenerThing();
            listener = null;

            GC.Collect();

            Assert.DoesNotThrow(() => MessageCenter<SomeTopic>.Publish(new SomeTopic()));
        }

        [Test]
        public void OldListenersDoNotFire()
        {
            int x = 0;
            var listener = new Listener<SomeTopic>(topic => x++);
            listener = null; //No more listener references

            MessageCenter<SomeTopic>.Publish(new SomeTopic());

            Assert.AreEqual(0, x);
        }



        class SomeTopic : Topic
        {
            public int data;
        }

        class ListenerThing
        {
            public ListenerThing()
            {
                someTopicListener = new Listener<SomeTopic>(topic => this.data = topic.data);
            }

            public int data = 0;
            Listener<SomeTopic> someTopicListener;
        }
    }
}
