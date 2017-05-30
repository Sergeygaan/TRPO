using Core;
using Microsoft.Practices.Unity;
using MyPaint.Actions;
using MyPaint.Build;
using MyPaint.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ActivForm
{
    public class InitializationData
    {

        /// <summary>
        /// Переменная, хранящая список классов для построения различных фигур.
        /// </summary>
        private List<IFigureBuild> _figuresBuild = new List<IFigureBuild>();


        /// <summary>
        /// Переменная, хранящая список действий для построения различных фигур.
        /// </summary>
        private List<IActoins> _actionsBuild = new List<IActoins>();

        private Сommands _commandClass;

        private ParameterChanges _parameterChangesClass;

        /// <summary>
        /// Переменная, хранящая класс для отрисовки и сохранения фигур.
        /// </summary>
        private DrawPaint _drawClass;

        /// <summary>
        /// Переменная, хранящая класс для выделения.
        /// </summary>
        private SelectDraw _selectClass;

        private ActivChildForm _activFormMain;

        public InitializationData(int Width, int Height)
        {
            var unityContainerInit = new UnityContainer();

            _commandClass = unityContainerInit.Resolve<Сommands>();

            _selectClass = unityContainerInit.Resolve<SelectDraw>();

            _drawClass = unityContainerInit.Resolve<DrawPaint>(new OrderedParametersOverride(new object[] { Width, Height, _commandClass }));

            _parameterChangesClass = unityContainerInit.Resolve<ParameterChanges>(new OrderedParametersOverride(new object[] { _drawClass, _commandClass }));

            _figuresBuild.Add(unityContainerInit.Resolve<Rectangles>());
            _figuresBuild.Add(unityContainerInit.Resolve<Ellipses>());
            _figuresBuild.Add(unityContainerInit.Resolve<Line>());
            _figuresBuild.Add(unityContainerInit.Resolve<PoliLine>());
            _figuresBuild.Add(unityContainerInit.Resolve<Polygon>());
            _figuresBuild.Add(unityContainerInit.Resolve<RectangleSelect>());

            _actionsBuild.Add(unityContainerInit.Resolve<DrawActoins>(new OrderedParametersOverride(new object[] { _figuresBuild, _selectClass, _drawClass })));
            _actionsBuild.Add(unityContainerInit.Resolve<SelectRegionActions>(new OrderedParametersOverride(new object[] { _figuresBuild, _selectClass, _drawClass, _parameterChangesClass })));
            _actionsBuild.Add(unityContainerInit.Resolve<SelectPointActions>(new OrderedParametersOverride(new object[] { _figuresBuild, _selectClass, _drawClass, _parameterChangesClass })));

            _activFormMain = new ActivChildForm(_selectClass, _drawClass, _parameterChangesClass, _figuresBuild, _actionsBuild);
        }

        public ActivChildForm ReturnActivClass()
        {
            return _activFormMain;
        }
    }
}
