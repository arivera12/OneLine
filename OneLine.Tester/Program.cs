using OneLine.Bases;
using OneLine.Extensions;
using OneLine.Models;
using OneLine.Validations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneLine.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var testForm = new TestForm();
            new Action(async () => 
            { 
                await testForm.ValidateBlobDatas(); 
                await testForm.AddBlobDatas();
                await testForm.ClearBlobDatas();
                //await testForm.Save(); 
            }).Invoke();


            //var testdataView = new TestDataView();
            //new Action(async () => await testdataView.Search()).Invoke();

        }
    }



    public class TestDataView : DataViewBase<TestModel, Identifier<string>, string, TestHttpService, BlobData, BlobDataValidator, UserBlobs>
    {

    }

    public class TestForm : FormBase<TestModel, Identifier<string>, string, TestHttpService, BlobData, BlobDataValidator, UserBlobs>
    {
        public Tuple<IEnumerable<BlobData>, FormFileRules> TestBlobData { get; set; } = Tuple.Create(new List<BlobData>().AsEnumerable(), new FormFileRules(true, 1, 1, 1048576));

        
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
