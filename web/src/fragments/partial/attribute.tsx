import { IAttribute, IAttributeArgument } from "../../structure";
import { ReactFragment, FormatContext, join, keyword, literal, location } from ".";

function attributeTarget(value: IAttribute): ReactFragment {
    return value.t ? [keyword(value.t), ': '] : null;
}

function attributeConstructorArgument(context: FormatContext, value: IAttributeArgument): ReactFragment {
    if (value.n)
        return [value.n, ' = ', literal(context, value.v)]
    else
        return literal(context, value.v);
}

export function attribute(context: FormatContext, value: IAttribute): ReactFragment {
    return [
        '[',
        attributeTarget(value),
        location(context, value.l, value.n),
        value.a ?
            ['(', join(value.a.map(x => attributeConstructorArgument(context, x)), ', '), ')'] :
            null,
        ']'
    ];
}
