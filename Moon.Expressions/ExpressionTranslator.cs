using System.Linq.Expressions;
using Moon.Expressions.ExpressionHandlers;
namespace Moon.Expressions;

public class ExpressionTranslator : ExpressionVisitor
{
	private readonly IExpressionTypeCalculator _expressionTypeCalculator;
    private readonly Dictionary<BinaryExpressionType, IBinaryExpressionHandler> _binaryExpressionHandlers;
	private readonly Dictionary<UnaryExpressionType, IUnaryExpressionHandler> _unaryExpressionHandlers;
	private readonly Dictionary<ConstantExpressionType, IConstantExpressionHandler> _constantExpressionHandlers;
	private readonly Dictionary<MethodCallExpressionType, IMethodCallExpressionHandler> _methodCallExpressionHandlers;

	private ISqlExpression state;

    public ExpressionTranslator(IExpressionTypeCalculator expressionTypeCalculator, IEnumerable<IBinaryExpressionHandler> binaryExpressionHandlers)
	{
		_expressionTypeCalculator = expressionTypeCalculator ?? throw new ArgumentNullException(nameof(expressionTypeCalculator));
        _binaryExpressionHandlers = binaryExpressionHandlers?.ToDictionary(x => x.ExpressionType) ?? throw new ArgumentNullException(nameof(binaryExpressionHandlers));
    }

	public ISqlExpression Translate(Expression expression)
	{
		Visit(expression);
		return state;
	}

    protected override Expression VisitBinary(BinaryExpression node)
    {
		var left = Translate(node.Left);
		var right = Translate(node.Right);

		var expressionType = _expressionTypeCalculator.Calculate(node);
		var expression = _binaryExpressionHandlers[expressionType].Handle(left, right);

		state = expression;
		return node;
    }

    protected override Expression VisitUnary(UnaryExpression node)
    {
        var operand = Translate(node.Operand);

		var expressionType = _expressionTypeCalculator.Calculate(node);
		var expression = _unaryExpressionHandlers[expressionType].Handle(operand);

		state = expression;
		return node;
    }

	protected override Expression VisitConstant(ConstantExpression node)
	{
		var expressionType = _expressionTypeCalculator.Calculate(node);
		var expression = _constantExpressionHandlers[expressionType].Handle(node.Value);

		state = expression;
		return node;
	}

	protected override Expression VisitMethodCall(MethodCallExpression node)
	{
		var arguments = node.Arguments.Select(Translate).ToArray();
		var caller = node.Object != null ? Translate(node.Object) : null;

		var expressionType = _expressionTypeCalculator.Calculate(node);
		var expression = _methodCallExpressionHandlers[expressionType].Handle(caller, arguments);

		state = expression;
		return node;
	}
}
