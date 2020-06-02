using OneLine.Bases;
using OneLine.Models;
using OneLine.Validations;
using System;

namespace OneLine.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            //var testForm = new TestForm();
            //new Action(async () => await testForm.Save()).Invoke();

            var testdataView = new TestDataView();
            new Action(async () => await testdataView.Search()).Invoke();

        }
    }



    public class TestDataView : DataViewBase<TestModel, Identifier<string>, string, TestHttpService, BlobData, BlobDataValidator, UserBlobs>
    {

    }

    public class TestForm : FormBase<TestModel, Identifier<string>, string, TestHttpService, BlobData, BlobDataValidator, UserBlobs>
    {

    }

    public class TestModel
    {
        public string Id { get; set; }
        public int MyProperty { get; set; }
    }

    public class TestHttpService : HttpBaseCrudExtendedService<TestModel, Identifier<string>, string, BlobData, BlobDataValidator, UserBlobs>
    {

    }
}
