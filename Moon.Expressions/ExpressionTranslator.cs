using System.Linq.Expressions;
using Moon.Expressions.ExpressionHandlers;
namespace Moon.Expressions;

public class ExpressionTranslator : ExpressionVisitor
{
	private readonly IExpressionTypeCalculator _expressionTypeCalculator;
	private readonly Dictionary<FullExpressionType, IBinaryExpressionHandler> _binaryExpressionHandlers;
	private readonly Dictionary<FullExpressionType, IUnaryExpressionHandler> _unaryExpressionHandlers;
	private readonly Dictionary<FullExpressionType, IConstantExpressionHandler> _constantExpressionHandlers;
	private readonly Dictionary<FullExpressionType, IMethodCallExpressionHandler> _methodCallExpressionHandlers;
	private readonly Dictionary<FullExpressionType, IMemberExpressionHandler> _memberExpressionHandlers;
	private readonly Dictionary<FullExpressionType, IParameterExpressionHandler> _parameterExpressionHandlers;

	private SqlExpression state;

    public ExpressionTranslator(
		IExpressionTypeCalculator expressionTypeCalculator, 
		IEnumerable<IBinaryExpressionHandler> binaryExpressionHandlers, 
		IEnumerable<IUnaryExpressionHandler> unaryExpressionHandlers,
		IEnumerable<IConstantExpressionHandler> constantExpressionHandlers,
		IEnumerable<IMethodCallExpressionHandler> methodCallExpressionHandlers,
		IEnumerable<IMemberExpressionHandler> memberExpressionHandlers,
		IEnumerable<IParameterExpressionHandler> parameterExpressionHandlers)
	{
		_expressionTypeCalculator = expressionTypeCalculator ?? throw new ArgumentNullException(nameof(expressionTypeCalculator));
        _binaryExpressionHandlers = binaryExpressionHandlers?.ToDictionary(x => x.ExpressionType) ?? throw new ArgumentNullException(nameof(binaryExpressionHandlers));
		_unaryExpressionHandlers = unaryExpressionHandlers?.ToDictionary(x => x.ExpressionType) ?? throw new ArgumentNullException(nameof(unaryExpressionHandlers));
		_constantExpressionHandlers = constantExpressionHandlers?.ToDictionary(x => x.ExpressionType) ?? throw new ArgumentNullException(nameof(constantExpressionHandlers));
		_methodCallExpressionHandlers = methodCallExpressionHandlers?.ToDictionary(x => x.ExpressionType) ?? throw new ArgumentNullException(nameof(methodCallExpressionHandlers));
		_memberExpressionHandlers = memberExpressionHandlers?.ToDictionary(x => x.ExpressionType) ?? throw new ArgumentNullException(nameof(memberExpressionHandlers));
		_parameterExpressionHandlers = parameterExpressionHandlers?.ToDictionary(x => x.ExpressionType) ?? throw new ArgumentNullException(nameof(parameterExpressionHandlers));
    }

	public SqlExpression Translate(Expression expression)
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

	protected override Expression VisitMember(MemberExpression node)
	{
		var memberName = node.Member.Name;
		var innerExpression = Translate(node.Expression);

		var expressionType = _expressionTypeCalculator.Calculate(node);
		var expression = _memberExpressionHandlers[expressionType].Handle(memberName, innerExpression);

		state = expression;
		return node;
	}

    protected override Expression VisitParameter(ParameterExpression node)
    {
		var name = node.Name;

		var expressionType = _expressionTypeCalculator.Calculate(node);
		var expression = _parameterExpressionHandlers[expressionType].Handle(name);

		state = expression;
		return node;
    }
}
