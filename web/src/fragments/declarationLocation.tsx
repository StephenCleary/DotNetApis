import * as React from "react";

import { IEntity, ITopLevelEntityBase } from "../structure";
import { PackageContext, FormatContext, Styles } from "../util";
import { fullConcreteTypeReference } from "./partial";

export function declarationLocation(pkgContext: PackageContext, entity: IEntity): React.ReactNode[] {
    const context = new FormatContext(pkgContext, Styles.DECLARATION);
    const parent = pkgContext.pkg.findEntityParent(entity.i);
    const result = parent ? fullConcreteTypeReference(context, parent) : (entity as ITopLevelEntityBase).s as string;
    return React.Children.toArray(result);
}
