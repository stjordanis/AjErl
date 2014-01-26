﻿namespace AjErl.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;

    public class ListsModule : Module
    {
        public ListsModule(Context context)
            : base(context)
        {
            this.SetName("lists");
            this.Context.SetValue("map/2", new FuncFunction(Map));
            this.Context.SetValue("filter/2", new FuncFunction(Filter));
        }

        private static object Map(Context context, IList<object> arguments)
        {
            IFunction function = (IFunction)arguments[0];
            List list = (List)arguments[1];
            IList<object> elements = new List<object>();

            while (list != null)
            {
                elements.Add(function.Apply(context, new object[] { list.Head }));
                list = (List)list.Tail;
            }

            return List.MakeList(elements);
        }

        private static object Filter(Context context, IList<object> arguments)
        {
            IFunction function = (IFunction)arguments[0];
            List list = (List)arguments[1];
            IList<object> elements = new List<object>();

            while (list != null)
            {
                var result = function.Apply(context, new object[] { list.Head });

                if (true.Equals(result))
                    elements.Add(list.Head);

                list = (List)list.Tail;
            }

            return List.MakeList(elements);
        }
    }
}